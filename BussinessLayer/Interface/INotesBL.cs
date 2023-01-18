using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity CreateNotes(CreateNoteModel createNoteModel, long userId);
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId);
        public NotesEntity UpdateNotes(long noteId, long userId, CreateNoteModel createNoteModel);
        public NotesEntity DeleteNotes(long noteId,long userId);    
        public bool PinNote(long noteId);
        public bool Trash(long noteId);
        public bool ArchiveNote(long noteId);
        public NotesEntity ColorChangeNote(long noteId,string color);
    }
}
