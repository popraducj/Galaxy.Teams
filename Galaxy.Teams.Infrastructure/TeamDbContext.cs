using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Galaxy.Teams.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Galaxy.Teams.Infrastructure
{
    public class TeamDbContext :DbContext
    {
        public TeamDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        public DbSet<Team> Teams { get; set; }
        public DbSet<Robot> Robots { get; set; }
        public DbSet<Captain> Captains { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddConvertedProperties(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void AddConvertedProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().Property(x => x.Robots).HasConversion(
                new ValueConverter<List<Guid>, string>(
                    v => v == null? null: JsonConvert.SerializeObject(v),
                    v => v== null ? null: JsonConvert.DeserializeObject<List<Guid>>(v)));
        }
    }
}