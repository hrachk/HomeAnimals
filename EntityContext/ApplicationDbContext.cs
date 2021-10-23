using System;
using HomeAnimals.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HomeAnimals.Models
{
    public partial class ApplicationDbContext : DbContext, IApplicationDbContext
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
       public DbSet<V_EvidenceOwner> Evidences { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnimalBirthDate)
                    .HasColumnType("date")
                    .HasColumnName("animalBirthDate");

                entity.Property(e => e.AnimalBreed)
                    .HasMaxLength(20)
                    .HasColumnName("animalBreed");

                entity.Property(e => e.AnimalGender)
                    .HasMaxLength(15)
                    .HasColumnName("animalGender");

                entity.Property(e => e.AnimalId).HasColumnName("animalID");

                entity.Property(e => e.AnimalKind)
                    .HasMaxLength(20)
                    .HasColumnName("animalKind");

                entity.Property(e => e.AnimalName)
                    .HasMaxLength(30)
                    .HasColumnName("animalName");

                entity.Property(e => e.CatchingMouses).HasColumnName("catchingMouses");

                entity.Property(e => e.LevelOfTraining).HasColumnName("levelOfTraining");

                entity.Property(e => e.NumberFeedings).HasColumnName("numberFeedings");

                entity.Property(e => e.OwnerId).HasColumnName("ownerID");
            });  
            
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Addres)
                    .HasMaxLength(60)
                    .HasColumnName("addres");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.OwnerId).HasColumnName("ownerId");

                entity.Property(e => e.OwnerKind)
                    .HasMaxLength(10)
                    .HasColumnName("ownerKind");

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(50)
                    .HasColumnName("ownerName");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public void UpdateChanges()
        {
             SaveChanges();
        }
    }
}
