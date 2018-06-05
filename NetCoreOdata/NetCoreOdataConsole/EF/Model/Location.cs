using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace NetCoreOdataConsole
{
    [Table("location")]
    public partial class Location
    {

        public Location()
        {
            this.Historicallocation = new HashSet<Historicallocation>();
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

        [MaxLength(100)]
        [Column("Location")]
        public string LocationJson { get; set; }

        [JsonIgnore]
        public virtual ICollection<Historicallocation> Historicallocation { get; set; }
    }
}
