using ElevenNote.Data;
using ElevenNote.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var e = new Note()
            {
                OwnerId = _userId,
                Title = model.Title,
                Content = model.Content,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(e);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Notes
                    .Where(x => x.OwnerId == _userId)
                    .Select(
                        x => new NoteListItem()
                        {
                            NoteId = x.NoteId,
                            Title = x.Title,
                            CreatedUtc = x.CreatedUtc
                        }
                    );
                return query.ToArray();
            }
        }

        public NoteDetail GetNoteById(int id)
        {
           using (var ctx = new ApplicationDbContext())
            {
                var dbRow = ctx.Notes
                    .Single(n => n.NoteId == id && n.OwnerId == _userId);
                if(dbRow != null)
                {
                    return new NoteDetail
                    {
                        NoteId = dbRow.NoteId,
                        Title = dbRow.Title,
                        Content = dbRow.Content,
                        CreatedUtc = dbRow.CreatedUtc,
                        ModifiedUtc = dbRow.ModifiedUtc
                    };
                }
                
                else
                    //add logging
                    return null;
               
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var dbRow = ctx.Notes
                    .Single(n => n.NoteId == model.NoteId && n.OwnerId == _userId);

                dbRow.Title = model.Title;
                dbRow.Content = model.Content;
                dbRow.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var row2Delete = ctx.Notes.Find(id);
                if (row2Delete.OwnerId == _userId)
                    ctx.Notes.Remove(row2Delete);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
