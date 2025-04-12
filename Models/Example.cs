using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexify.Models
{
    public class Example
    {
        [Key]
        public int ExampleID { get; set; }

        public int WordID { get; set; }

        [Required]
        public string ExampleText { get; set; }

        // Foreign key relationship
        [ForeignKey("WordID")]
        public virtual Word Word { get; set; }
    }
}