using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ILabelBL
    {
        public bool CreateLabel(long noteId, long userId, string labelName);
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId);
        public bool UpdateLabel(long userId, UpdateLabel update);

    }
}
