using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL:IUserRL
    {
        private readonly FunDooContext fundooContext;
        private readonly string _secret;
        private readonly string _expDate;

        private readonly IConfiguration iconfiguration;
        private bool x;

        public UserRL(FunDooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
            _secret = iconfiguration.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = iconfiguration.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntityobj = new UserEntity();
                userEntityobj.FirstName = userRegistrationModel.FirstName;
                userEntityobj.LastName = userRegistrationModel.LastName;
                userEntityobj.Email = userRegistrationModel.Email;
                userEntityobj.Password = userRegistrationModel.Password;
                fundooContext.Users.Add(userEntityobj);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
                    return userEntityobj;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                var result = fundooContext.Users.Where(x => x.Email == userLogin.Email && x.Password == userLogin.Password).FirstOrDefault();
                if(result != null && result.Password==userLogin.Password)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GenerateSecurityToken(string email,long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public string ForgotPassword(string email)
        {
            try
            {
                var result = fundooContext.Users.Where(x => x.Email == email).FirstOrDefault();
                if(result!=null)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    MSMQ_Model mq = new MSMQ_Model();
                    mq.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email, string new_password, string confirm_password)
        {
            try
            {
                if (new_password == confirm_password)
                {
                    var result = fundooContext.Users.Where(x => x.Email == email).FirstOrDefault();
                    result.Password = new_password;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
