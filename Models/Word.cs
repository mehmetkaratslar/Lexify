using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexify.Models
{
    public class Word
    {
        [Key]
        public int WordID { get; set; }

        [Required]
        [StringLength(100)]
        public string WordText { get; set; }

        [StringLength(50)]
        public string WordType { get; set; } // İsim, fiil, sıfat, vb.  

        [StringLength(50)]
        public string Language { get; set; }

        [StringLength(20)]
        public string ColorCode { get; set; } // Renk kodu (#RRGGBB)  

        [StringLength(20)]
        public string LearningStatus { get; set; } // Yeni, Öğreniliyor, Öğrenildi  

        public DateTime DateAdded { get; set; }

        // Navigation properties  
        public virtual ICollection<Definition> Definitions { get; set; } = new List<Definition>();
        public virtual ICollection<Example> Examples { get; set; } = new List<Example>();

        // İlişkili kelimeler (eş/zıt anlamlılar)  
        public virtual ICollection<WordRelation> RelatedWordsFrom { get; set; } = new List<WordRelation>();
        public virtual ICollection<WordRelation> RelatedWordsTo { get; set; } = new List<WordRelation>();
    }
}