using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreOdataConsole
{
    [Table("sensor")]
    public partial class Sensor
    {
        public Sensor()
        {
            this.Datastream = new HashSet<Datastream>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string EncodingType { get; set; }

        [MaxLength(150)]
        public string MetaData { get; set; }

        [JsonIgnore]
        public virtual ICollection<Datastream> Datastream { get; set; }
    }
}
