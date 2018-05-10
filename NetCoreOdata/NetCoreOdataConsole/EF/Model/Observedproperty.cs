using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreOdataConsole
{
    [Table("observedproperty")]
    public partial class Observedproperty
    {

        public Observedproperty()
        {
            this.Datastream = new HashSet<Datastream>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Definition { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Datastream> Datastream { get; set; }
    }
}
