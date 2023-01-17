using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity CreateNotes(CreateNoteModel createNodeMidel,long userId);
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId);
    }
}
