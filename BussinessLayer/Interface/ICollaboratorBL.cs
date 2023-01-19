using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ICollaboratorBL
    {
        public CollaboratorEntity CreateCollab(long noteId, string email);
        public IEnumerable<CollaboratorEntity> RetrieveCollab(long noteId);
    }
}
