﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreOdataConsole
{
    [Table("historicallocation")]
    public partial class Historicallocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid Thing_Id { get; set; }

        [Required]
        public Guid Location_Id { get; set; }

        public DateTime? Time { get; set; }

        [JsonIgnore]
        [ForeignKey("Location_Id")]
        public virtual Location Location { get; set; }

        [JsonIgnore]
        [ForeignKey("Thing_Id")]
        public virtual Thing Thing { get; set; }
    }
}
