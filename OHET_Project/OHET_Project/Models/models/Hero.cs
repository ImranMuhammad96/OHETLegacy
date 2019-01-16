using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    [DebuggerStepThrough]
    public class Hero
    {
        public Hero()
        {
            //this.classes = new HashSet<Class>();
            this.classes = new Collection<Class>();
        }
        
        [Key]
        public int IDHero { get; set; }

        [Required]
        [StringLength(500)]
        public string name { get; set; }

        [Required]
        [Range(7, 100)]
        public int conceptLvl { get; set; }

        [Required]
        [StringLength(5000)]
        public string description { get; set; }

        [Required]
        public AttributeLvl StrAttribute { get; set; }

        [Required]
        public AttributeLvl DexAttribute { get; set; }

        [Required]
        public AttributeLvl ConAttribute { get; set; }

        [Required]
        public AttributeLvl IntAttribute { get; set; }

        [Required]
        public AttributeLvl WisAttribute { get; set; }

        [Required]
        public AttributeLvl ChaAttribute { get; set; }

        public List<String> equipment { get; set; }

        public int gold { get; set; }

        public int exp { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }

        public virtual ICollection<Class> classes { get; set; }
    }
}