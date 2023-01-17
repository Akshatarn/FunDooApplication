using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }       

        public string Login(UserLogin userLogin)
        {
            try
            {
                return iuserRL.Login(userLogin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ForgotPassword(string email)
        {
            try
            {
                return iuserRL.ForgotPassword(email);
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
                return iuserRL.ResetPassword(email, new_password, confirm_password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
