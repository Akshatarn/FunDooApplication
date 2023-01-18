using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class NotesBL : INotesBL
    {
        INotesRL iNoteRL;
        public NotesBL(INotesRL iNoteRL)
        {
            this.iNoteRL = iNoteRL;
        }
        public NotesEntity CreateNotes(CreateNoteModel createNoteModel,long userId)
        {
            try
            {
                return iNoteRL.CreateNotes(createNoteModel,userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId)
        {
            try
            {
                return iNoteRL.RetrieveNotes(userId, noteId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public NotesEntity UpdateNotes(long noteId, long userId, CreateNoteModel createNoteModel)
        {
            try
            {
                return this.iNoteRL.UpdateNotes(noteId, userId, createNoteModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity DeleteNotes(long noteId, long userId)
        {
            try
            {
                return this.iNoteRL.DeleteNotes(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool PinNote(long noteId)
        {
            try
            {
                return this.iNoteRL.PinNote(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
