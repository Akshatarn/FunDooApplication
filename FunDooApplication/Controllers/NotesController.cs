using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FunDooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesBL iNoteBL;
        public NotesController(INotesBL iNoteBL)
        {
            this.iNoteBL = iNoteBL;
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
        
    }
}
