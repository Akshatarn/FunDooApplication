using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Registration(UserRegistrationModel userRegistrationModel);
        public string Login(UserLogin userLogin);
        public string ForgotPassword(string email);
        public bool ResetPassword(string email, string new_password, string confirm_password);
    }
}
