using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryLayer.Services
{
    public class LabelRL:ILabelRL
    {
        private readonly FunDooContext funDooContext;
        public LabelRL(FunDooContext funDooContext)
        {
            this.funDooContext = funDooContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NoteID"></param>
        /// <param name="userId"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
        public LabelEntity CreateLabel(long noteId,long UserId,string labelName)
        {
            try
            {
                var notesresult = funDooContext.Notes.Where( x=>x.NoteID == noteId).FirstOrDefault();
                var userresult = funDooContext.Users.Where( x=>x.UserId == UserId).FirstOrDefault();
                if (notesresult!=null && userresult!=null)
                {
                    LabelEntity labelEntity= new LabelEntity(); ;
                    labelEntity.NoteID= notesresult.NoteID;
                    labelEntity.UserId= userresult.UserId;
                    labelEntity.LabelName= labelName;
                    funDooContext.Label.Add(labelEntity);
                    funDooContext.SaveChanges();
                    return labelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LabelID"></param>
        /// <returns></returns>
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId)
        {
            try
            {
                var result = funDooContext.Label.Where(e => e.LabelId == labelId).ToList();
                return result;
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
                var result = funDooContext.Label.Where(e => e.UserId == userId && e.LabelName == update.OldLabelName).FirstOrDefault();
                if (result != null)
                {
                    result.LabelName = update.NewLabelName;
                    funDooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LabelID"></param>
        /// <returns></returns>
        public bool DeleteLabel(long labelId)
        {
            try
            {
                var result = funDooContext.Label.FirstOrDefault(e => e.LabelId == labelId);
                if (result != null)
                {
                    funDooContext.Label.Remove(result);
                    funDooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

