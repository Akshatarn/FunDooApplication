using BussinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class LabelBL:ILabelBL
    {
        private readonly ILabelRL ilabelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.ilabelRL = labelRL;
        }
        public bool CreateLabel(long noteId,long userId,string labelName)
        {
            try
            {
                return this.ilabelRL.CreateLabel(noteId, userId, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
