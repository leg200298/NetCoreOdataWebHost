using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetCoreOdataConsole
{
    public class DBContext : DbContext
    {
        private string _connectionString;
        private IConfiguration _configuration;

        public DBContext(string connection)
        {
            _connectionString = connection;
        }
        public DBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
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
