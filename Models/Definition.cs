using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lexify.Models
{
    public class Definition
    {
        [Key]
        public int DefinitionID { get; set; }

        public int WordID { get; set; }

        [Required]
        public string DefinitionText { get; set; }

        // Foreign key relationship
        [ForeignKey("WordID")]
        public virtual Word Word { get; set; }
    }
}