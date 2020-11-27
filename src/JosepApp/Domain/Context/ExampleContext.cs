using JosepApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JosepApp.Domain.Context
{
    public class ExampleContext: DbContext
    {
        public DbSet<Example> Examples { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Example>().ToTable("Example", "test");
            modelBuilder.Entity<Example>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ExampleString);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
