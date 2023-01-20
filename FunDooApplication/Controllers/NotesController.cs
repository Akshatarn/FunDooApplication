using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace FunDooApplication.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesBL iNoteBL;
        //private readonly IMemoryCache memoryCache;
        private readonly FunDooContext funDooContext;
        private readonly IDistributedCache distributedCache;
        public NotesController(INotesBL iNoteBL,IDistributedCache distributedCache,FunDooContext funDooContext)
        {
            this.iNoteBL = iNoteBL;
           // this.memoryCache = memoryCache;
            this.distributedCache= distributedCache;
            this.funDooContext= funDooContext;

        }
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "notesList";
            string serializedNotesList;
            var notesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                notesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                notesList = await funDooContext.Notes.ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(notesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(notesList);
        }
        [Authorize]
        [HttpPost] //Entring the data in the database
        [Route("CreateNote")]
        public IActionResult CreateNote(CreateNoteModel createNodeModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNoteBL.CreateNotes(createNodeModel,userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Create Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Create Unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("RetrieveNote")]
        public IActionResult RetrieveNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNoteBL.RetrieveNotes(userId, noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Get Notes Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to get Note." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("UpdateNote")]
        public IActionResult UpdateNotes(long noteId, CreateNoteModel createNoteModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNoteBL.UpdateNotes(noteId, userId, createNoteModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Update Notes Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Update Note." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNoteBL.DeleteNotes(noteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Deleted Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Delete Note." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Pinned-Unpinned")]
        public IActionResult PinNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNoteBL.PinNote(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Pinned Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "UnPinned." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Trashed")]
        public IActionResult Trash(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNoteBL.PinNote(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Trashed Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Trash Unsuccesfull." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("ArchieveOrUnArchieve")]
        public IActionResult ArchiveNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNoteBL.ArchiveNote(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Archieved Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Archieve Unsuccesfull." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("ChangeColor")]
        public IActionResult ColorChangeNote(long noteId,string color)
        {
            try
            {
                var result = iNoteBL.ColorChangeNote(noteId,color);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Color Changed Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Color Change Unsuccesfull." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage( [FromQuery]NoteIdModelModel noteIdModel,IFormFile image)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNoteBL.UploadImage(userId, noteIdModel, image);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Image Uploaded Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Image Upload Unsuccesfull." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
