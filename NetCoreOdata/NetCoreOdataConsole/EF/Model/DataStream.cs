﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCoreOdataConsole
{
    public partial class Datastream
    {
        public Datastream()
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

        [MaxLength(150)]
        public string ObservationType { get; set; }

        [MaxLength(150)]
        public string UnitOfMeasurement { get; set; }

        [MaxLength(150)]
        public string ObservedArea { get; set; }

        public DateTime? PhenomenonTime { get; set; }

        public DateTime? ResultTime { get; set; }

        [Required]
        public int Thing_Id { get; set; }

        [Required]
        public int Sensor_Id { get; set; }

        [Required]
        public int ObervedProperty_Id { get; set; }

        [JsonIgnore]
        [ForeignKey("ObervedProperty_Id")]
        public virtual Observedproperty Observedproperty { get; set; }

        [JsonIgnore]
        [ForeignKey("Sensor_Id")]
        public virtual Sensor Sensor { get; set; }

        [JsonIgnore]
        [ForeignKey("Thing_Id")]
        public virtual Thing Thing { get; set; }

        [JsonIgnore]
        public virtual ICollection<Observation> Observation { get; set; }

    }
}