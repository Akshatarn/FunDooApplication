using BussinessLayer.Interface;
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
    public class CollaboratorController : ControllerBase
    {
        public readonly ICollaboratorBL icollabBL;
        public readonly FunDooContext funDooContext;
        public CollaboratorController(ICollaboratorBL icollabBL,FunDooContext funDooContext)
        {
            this.icollabBL = icollabBL;
            this.funDooContext = funDooContext;
        }
        [Authorize]
        [HttpPost]
        [Route("Create-Collaborator")]
        public IActionResult CreateCollab(long noteId,string email)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icollabBL.CreateCollab(noteId, email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborator Created", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to create collaborator" });

                }
            }
            catch(Exception)
            {
                throw;
            }
            
        }
        [Authorize]
        [HttpGet]
        [Route("Retrieve-Collaborator")]
        public IActionResult RetrieveCollab(long noteId)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = icollabBL.RetrieveCollab(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborator Retrieved", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Retrieve collaborator" });

                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        [Authorize]
        [HttpDelete]
        [Route("Remove-Collaborator")]
        public IActionResult DeleteCollab(long collabId)
        {
            try
            {
                var result = icollabBL.DeleteCollab(collabId);
                if(result!=null)
                {
                    return Ok(new { success = true, message = "Collaborator Removed", data = result });

                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Remove collaborator" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
