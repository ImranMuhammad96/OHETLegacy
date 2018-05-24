using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public partial class Hero
    {

        public Hero()
        {
            this.attributes = new HashSet<Attribute>();
            this.classes = new HashSet<Class>();
        }

        [Key]
        public int IDHero { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int armorClassSum { get; set; }

        [Required]
        public int gold { get; set; }

        [Required]
        public int exp { get; set; }

        [ForeignKey("IDConcept")]
        public Concept Concept { get; set; }
        public int? IDConcept { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }

        public ICollection<Equipment> Equipment { get; set; }
        public virtual ICollection<Attribute> attributes { get; set; }
        public ICollection<HL> HL { get; set; }
        public virtual ICollection<Class> classes { get; set; }
    }
}