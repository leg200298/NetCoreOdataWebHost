using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreOdataConsole
{
    [Table("featureofinterest")]
    public partial class Featureofinterest
    {

        public Featureofinterest()
        {
            this.Observation = new HashSet<Observation>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string EncodingType { get; set; }

        [MaxLength(150)]
        public string Feature { get; set; }

        [JsonIgnore]
        public virtual ICollection<Observation> Observation { get; set; }
    }
}
