using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId)
        {
            try
            {
                return this.ilabelRL.RetrieveLabel(labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateLabel(long userId, UpdateLabel update)
        {
            try
            {
                return this.ilabelRL.UpdateLabel(userId, update);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteLabel(long labelId)
        {
            try
            {
                return this.ilabelRL.DeleteLabel(labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
