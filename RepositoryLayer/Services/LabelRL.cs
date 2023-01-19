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
        public bool CreateLabel(long noteId,long userId,string labelName)
        {
            try
            {
                var result = funDooContext.Labels.Where(e => e.userId == userId);
                if(result!=null)
                {
                    LabelEntity labelEntity= new LabelEntity(); ;
                    labelEntity.NoteID= noteId;
                    labelEntity.userId= userId;
                    labelEntity.LabelName= labelName;
                    funDooContext.Labels.Add(labelEntity);
                    int saveResult = funDooContext.SaveChanges();
                    if(saveResult>0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
        public IEnumerable<LabelEntity> RetrieveLabel(long labelId)
        {
            try
            {
                var result = funDooContext.Labels.Where(e => e.LabelId == labelId).ToList();
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
                var result = funDooContext.Labels.Where(e => e.userId == userId && e.LabelName == update.OldLabelName).FirstOrDefault();
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
        public bool DeleteLabel(long labelId)
        {
            try
            {
                var result = funDooContext.Labels.FirstOrDefault(e => e.LabelId == labelId);
                if (result != null)
                {
                    funDooContext.Labels.Remove(result);
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

