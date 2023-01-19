using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly FunDooContext funDooContext;
        private readonly IConfiguration iconfiguration;
        public CollaboratorRL(FunDooContext funDooContext, IConfiguration iconfiguration)
        {
            this.funDooContext = funDooContext;
            this.iconfiguration = iconfiguration;
        }
        public CollaboratorEntity CreateCollab(long noteId,string email)
        {
            try
            {
                var noteResult = funDooContext.Notes.Where(e=>e.NoteID == noteId).FirstOrDefault();
                var emailResult = funDooContext.Users.Where(e=>e.Email== email).FirstOrDefault();
                if(emailResult!= null && noteResult!=null)
                {
                    CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                    collaboratorEntity.UserId = emailResult.UserId;
                    collaboratorEntity.NoteID = noteResult.NoteID;
                    funDooContext.Collaborators.Add(collaboratorEntity);
                    int result = funDooContext.SaveChanges();
                    return collaboratorEntity;

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
        public IEnumerable<CollaboratorEntity> RetrieveCollab(long noteId)
        {
            try
            {
                var result = funDooContext.Collaborators.Where(e => e.NoteID == noteId).ToList();
                return result;
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
                var result = funDooContext.Collaborators.FirstOrDefault(e => e.CollabId == collabId);
                if(result!=null)
                {
                    funDooContext.Collaborators.Remove(result);
                    funDooContext.SaveChanges();
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
