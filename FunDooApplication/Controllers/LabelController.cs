using BussinessLayer.Interface;
using BussinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FunDooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilabelBL;
        private readonly FunDooContext funDooContext;
        public LabelController(ILabelBL ilabelBL, FunDooContext funDooContext)
        {
            this.ilabelBL = ilabelBL;
            this.funDooContext = funDooContext;
        }
        [Authorize]
        [HttpPost]
        [Route("Create-Label")]
        public IActionResult CreateLabel(long noteId,long userId,string labelname)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ilabelBL.CreateLabel(noteId, userId, labelname);
                if(result!=null)
                {
                    return Ok(new { success = true, message = "Label Created", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Not Created" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Retrieve-Label")]
        public IActionResult RetrieveLabel(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ilabelBL.RetrieveLabel(labelId);
                if(result!=null)
                {
                    return Ok(new { success = true, message = "Retrieve Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Retrieve Unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [Route("Update-Label")]
        public IActionResult UpdateLabel(UpdateLabel update)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = ilabelBL.UpdateLabel(userId, update);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Label Updated SuccessFully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Label not updated !" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
