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

    }

}
