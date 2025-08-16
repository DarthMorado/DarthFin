using DarthFin.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<FinEntryEntity> FinEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Rename the inherited Id property for UserEntity
            modelBuilder.Entity<UserEntity>()
                .Property(e => e.Id)
                .HasColumnName("USR_ID");

            modelBuilder.Entity<FileEntity>()
                .Property(e => e.Id)
                .HasColumnName("FIL_ID");

            modelBuilder.Entity<CategoryEntity>()
                .Property(e => e.Id)
                .HasColumnName("CAT_ID");

            modelBuilder.Entity<FinEntryEntity>()
                .Property(e => e.Id)
                .HasColumnName("FIN_ID");
        }
    }
}
