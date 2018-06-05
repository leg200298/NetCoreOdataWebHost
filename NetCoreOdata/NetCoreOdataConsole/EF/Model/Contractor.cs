using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreOdataConsole
{
    [Table("contractor")]
    class Contractor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Mail { get; set; }

        [Required]
        public Guid Thing_Id { get; set; }

        [JsonIgnore]
        [ForeignKey("Thing_Id")]
        public virtual Thing Thing { get; set; }
    }
}
