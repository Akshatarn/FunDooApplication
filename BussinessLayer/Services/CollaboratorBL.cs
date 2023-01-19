using BussinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class CollaboratorBL : ICollaboratorBL
    {
        private readonly ICollaboratorRL icollabRL;
        public CollaboratorBL(ICollaboratorRL icollabRL)
        {
            this.icollabRL = icollabRL;
        }
        public CollaboratorEntity CreateCollab(long noteId,string email)
        {
            try
            {
                return this.icollabRL.CreateCollab(noteId, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
