using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        [Required]
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset CreatedTime { get; set; }

    }
}
