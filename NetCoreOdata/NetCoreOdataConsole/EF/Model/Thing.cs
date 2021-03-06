﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreOdataConsole
{
    [Table("thing")]
    public partial class Thing
    {

        public Thing()
        {
            this.Datastream = new HashSet<Datastream>();
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

        [MaxLength(150)]
        public string Properties { get; set; }

        [JsonIgnore]
        public virtual ICollection<Datastream> Datastream { get; set; }
        [JsonIgnore]
        public virtual ICollection<Historicallocation> Historicallocation { get; set; }

        [Required]
        public Guid IndustriaArea_Id { get; set; }

        [Required]
        public Guid Contractor_Id { get; set; }

        [JsonIgnore]
        [ForeignKey("Contractor_Id")]
        public virtual Contractor Contractor { get; set; }

        [JsonIgnore]
        [ForeignKey("IndustriaArea_Id")]
        public virtual IndustrialArea IndustriaArea { get; set; }
    }
}
