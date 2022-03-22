﻿using ElevenNote.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.NoteModels
{
    public class NoteListItem
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        [Display(Name="Created Date")]
        public DateTimeOffset CreatedUtc { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
