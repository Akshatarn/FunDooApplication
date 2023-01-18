using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        private readonly FunDooContext fundooContext;
        private readonly IConfiguration iconfiguration;
        public NotesRL(FunDooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }
        public NotesEntity CreateNotes(CreateNoteModel createNoteModel,long userId)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.UserId= userId;
                notesEntity.Title = createNoteModel.Title;
                notesEntity.Description = createNoteModel.Description;
                notesEntity.Reminder = createNoteModel.Reminder;
                notesEntity.Color = createNoteModel.Color;
                notesEntity.Image = createNoteModel.Image;
                notesEntity.Archive = createNoteModel.Archive;
                notesEntity.Pin = createNoteModel.Pin;
                notesEntity.Trash = createNoteModel.Trash;
                notesEntity.Created = createNoteModel.Created;
                notesEntity.Edited = createNoteModel.Edited;
                fundooContext.Notes.Add(notesEntity);
                int result = fundooContext.SaveChanges();
                if(result!=0)
                {
                    return notesEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<NotesEntity> RetrieveNotes(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.Notes.Where(e => e.UserId == userId);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public NotesEntity UpdateNotes(long noteId, long userId, CreateNoteModel createNoteModel)
        {
            try
            {
                var result = fundooContext.Notes.Where(x => x.UserId == userId && x.NoteID == noteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = createNoteModel.Title;
                    result.Description = createNoteModel.Description;
                    result.Reminder = createNoteModel.Reminder;
                    result.Color = createNoteModel.Color;
                    result.Image = createNoteModel.Image;
                    result.Archive = createNoteModel.Archive;
                    result.Pin = createNoteModel.Pin;
                    result.Trash = createNoteModel.Trash;
                    result.Created = createNoteModel.Created;
                    result.Edited = createNoteModel.Edited;
                    fundooContext.SaveChanges();
                    return result;
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
        public NotesEntity DeleteNotes(long noteId, long userId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(e => e.NoteID == noteId && e.UserId == userId);

                if (result != null)
                {

                    fundooContext.Notes.Remove(result);
                    fundooContext.SaveChanges();

                    return result;
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
        public bool PinNote(long noteId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(e => e.NoteID == noteId);

                if (result.Pin == true)
                {

                    result.Pin = false;
                    fundooContext.SaveChanges();

                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trash(long noteId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(e => e.NoteID == noteId);

                if (result.Trash == true)
                {

                    result.Trash = false;
                    fundooContext.SaveChanges();

                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ArchiveNote(long noteId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(e => e.NoteID == noteId);

                if (result.Archive == true)
                {

                    result.Archive = false;
                    fundooContext.SaveChanges();

                    return false;
                }
                else
                {
                    result.Archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity ColorChangeNote(long noteId, string color)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(e => e.NoteID == noteId);

                if (result.Color != null)
                {

                    result.Color = color;
                    fundooContext.SaveChanges();

                    return result;
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
        public string UploadImage(long userId, NoteIdModelModel noteIdModel, IFormFile image)
        {
            var result = fundooContext.Notes.Where(x => x.UserId == userId && x.NoteID == noteIdModel.NoteId).FirstOrDefault();
            if (result!=null)
            {
                Account account = new Account(
                    this.iconfiguration["CloudinarySettings:CloudName"],
                    this.iconfiguration["CloudinarySettings:ApiKey"],
                    this.iconfiguration["CloudinarySettings:ApiSecret"]
                    );
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadPictures = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream()),
                };
                var uploadResult = cloudinary.Upload(uploadPictures);
                string imagePath = uploadResult.Url.ToString();
                result.Image = imagePath;
                fundooContext.SaveChanges();
                return "Image Uploaded Successfully";
            }
            else
            {
                return null;
            }
        }

    }
}
