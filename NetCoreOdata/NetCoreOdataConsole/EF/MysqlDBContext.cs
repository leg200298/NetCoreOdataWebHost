using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreOdataConsole
{
    public class MysqlDBContext : DbContext
    {
        private string _connectionString;

        public MysqlDBContext(string connectionString)
        {
            _connectionString = connectionString; ;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        public virtual DbSet<Observation> Observation { get; set; }
        public virtual DbSet<Featureofinterest> Featureofinterest { get; set; }
        public virtual DbSet<Observedproperty> Observedproperty { get; set; }
        public virtual DbSet<Sensor> Sensor { get; set; }
        public virtual DbSet<Datastream> Datastream { get; set; }
        public virtual DbSet<Thing> Thing { get; set; }
        public virtual DbSet<Historicallocation> Historicallocation { get; set; }
        public virtual DbSet<Location> Location { get; set; }
    }
}
