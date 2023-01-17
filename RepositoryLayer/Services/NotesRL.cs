using CommonLayer.Model;
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
    public class NotesRL : INotesRL
    {
        private readonly FunDooContext fundooContext;
        private readonly IConfiguration iconfiguration;
        public NotesRL(FunDooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }
        public NotesEntity CreateNotes(CreateNoteModel createNoteModel,long userId)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.UserId= userId;
                notesEntity.Title = createNoteModel.Title;
                notesEntity.Description = createNoteModel.Description;
                notesEntity.Reminder = createNoteModel.Reminder;
                notesEntity.Color = createNoteModel.Color;
                notesEntity.Image = createNoteModel.Image;
                notesEntity.Archive = createNoteModel.Archive;
                notesEntity.Pin = createNoteModel.Pin;
                notesEntity.Trash = createNoteModel.Trash;
                notesEntity.Created = createNoteModel.Created;
                notesEntity.Edited = createNoteModel.Edited;
                fundooContext.Notes.Add(notesEntity);
                int result = fundooContext.SaveChanges();
                if(result!=0)
                {
                    return notesEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
