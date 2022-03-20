using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public enum Severity { High, Medium, Low}
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, MinLength(2, ErrorMessage = "Category namemust be at least 2 characters."), MaxLength(35, ErrorMessage = "Category name cannot be more than 35 characters.")]
        public string Name { get; set; }

        public Severity Severity { get; set; } = Severity.Medium;
    }
}
