using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity CreateLabel(long noteId, long UserId, string labelName);
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId);
        public bool UpdateLabel(long userId, UpdateLabel update);
        public bool DeleteLabel(long labelId);

    }
}
