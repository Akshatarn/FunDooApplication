using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace FunDooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;

        }
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration unsuccessful" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var result = iuserBL.Login(userLogin);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login successful", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Login unsuccessful" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = iuserBL.ForgotPassword(email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Mail sent successfully", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Mail sent unsuccessful" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult PasswordReset(string new_password, string confirm_password)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iuserBL.ResetPassword(email, new_password, confirm_password);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Password Reset Successfull" });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Password Reset Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
    
