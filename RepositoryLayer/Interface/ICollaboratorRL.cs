using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRL
    {
        public CollaboratorEntity CreateCollab(long noteId, string email);
        public IEnumerable<CollaboratorEntity> RetrieveCollab(long noteId);
        public bool DeleteCollab(long collabId);
    }
}
