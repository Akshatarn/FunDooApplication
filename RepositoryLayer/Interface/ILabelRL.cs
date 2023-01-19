using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public bool CreateLabel(long noteId, long userId, string labelName);
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId);
        public bool UpdateLabel(long userId, UpdateLabel update);

    }
}
