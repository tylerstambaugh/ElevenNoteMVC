using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models.CategoryModels
{
    public class CategoryDelete
    {
        [Required]
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
