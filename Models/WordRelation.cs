using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lexify.Services;
using System.Windows;

namespace Lexify.Models
{
    public class WordRelation
    {
        [Key]
        public int RelationID { get; set; }

        public int WordID { get; set; }

        public int RelatedWordID { get; set; }

        [StringLength(20)]
        public string RelationType { get; set; } // "Synonym" veya "Antonym"

        // Foreign key relationships
        [ForeignKey("WordID")]
        public virtual Word Word { get; set; }

        [ForeignKey("RelatedWordID")]
        public virtual Word RelatedWord { get; set; }
    }
}