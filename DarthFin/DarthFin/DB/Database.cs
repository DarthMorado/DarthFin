using DarthFin.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DarthFin.DB
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Rename the inherited Id property for UserEntity
            modelBuilder.Entity<UserEntity>()
                .Property(e => e.Id)
                .HasColumnName("USR_ID");

           
        }
    }
}
