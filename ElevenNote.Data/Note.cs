﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        [Required, Display(Name = "User Id")]
        public Guid OwnerId { get; set; }
        [Required, MaxLength(100, ErrorMessage ="Title must be less than 100 characters")]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required, Display(Name = "Created Date")]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
