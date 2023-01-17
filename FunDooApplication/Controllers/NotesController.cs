﻿using BussinessLayer.Interface;
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
        public IActionResult CreateNote(CreateNoteModel createNodeMidel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNoteBL.CreateNotes(createNodeMidel,userId);
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
    }
}