using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreOdataConsole
{
    [Table("observation")]
    public partial class Observation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime? Phenomenon_Time { get; set; }

        public DateTime? ResultTime { get; set; }

        [MaxLength(50)]
        public string Result { get; set; }

        [MaxLength(150)]
        public string ResultQuality { get; set; }

        [MaxLength(150)]
        public string ValidTime { get; set; }

        [MaxLength(300)]
        public string Parameters { get; set; }

        [Required]
        public Guid FeatureOflnterest_Id { get; set; }

        [Required]
        public Guid Datastream_Id { get; set; }

        [JsonIgnore]
        [ForeignKey("Datastream_Id")]
        public virtual Datastream Datastream { get; set; }

        [JsonIgnore]
        [ForeignKey("FeatureOflnterest_Id")]
        public virtual Featureofinterest Featureofinterest { get; set; }
    }
}
