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
        public IEnumerable<CollaboratorEntity> RetrieveCollab(long noteId)
        {
            try
            {
                return this.icollabRL.RetrieveCollab(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteCollab(long collabId)
        {
            try
            {
                return this.icollabRL.DeleteCollab(collabId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
