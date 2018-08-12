using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Hero
    {
        public Hero()
        {
            this.labels = new HashSet<Label>();
            this.attributes = new HashSet<Attribute>();
            this.classes = new HashSet<Class>();
        }
        

        [Key]
        public int IDHero { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int totalDR { get; set; }

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

        public virtual ICollection<Equipment> equipments { get; set; }
        public virtual ICollection<Label> labels { get; set; }
        public virtual ICollection<Attribute> attributes { get; set; }
        public virtual ICollection<Class> classes { get; set; }
    }
}