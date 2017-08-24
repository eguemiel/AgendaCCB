using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AgendaCCB.Data.Models
{
    public partial class agendaccbContext : DbContext
    {
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Collaborator> Collaborator { get; set; }
        public virtual DbSet<CommonCongregation> CommonCongregation { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumber { get; set; }
        public virtual DbSet<PhoneNumberCollaborator> PhoneNumberCollaborator { get; set; }
        public virtual DbSet<PositionMinistry> PositionMinistry { get; set; }
        public virtual DbSet<State> State { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=DART-JUNIOR\SQLEXPRESS;Database=agendaccb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdStateNavigation)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.IdState)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_City_State");
            });

            modelBuilder.Entity<Collaborator>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.IdCommonCongregationNavigation)
                    .WithMany(p => p.Collaborator)
                    .HasForeignKey(d => d.IdCommonCongregation)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Collaborator_CommonCongregation");

                entity.HasOne(d => d.IdPositionMinistyNavigation)
                    .WithMany(p => p.Collaborator)
                    .HasForeignKey(d => d.IdPositionMinisty)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Collaborator_PositionMinistry");
            });

            modelBuilder.Entity<CommonCongregation>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdCityNavigation)
                    .WithOne(p => p.CommonCongregation)
                    .HasForeignKey<CommonCongregation>(d => d.IdCity)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_CommonCongregation_City");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(20)");
            });

            modelBuilder.Entity<PhoneNumberCollaborator>(entity =>
            {
                entity.ToTable("PhoneNumber_Collaborator");

                entity.HasOne(d => d.IdCollaboratorNavigation)
                    .WithMany(p => p.PhoneNumberCollaborator)
                    .HasForeignKey(d => d.IdCollaborator)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhoneNumber_Collaborator_Collaborator");

                entity.HasOne(d => d.IdPhoneNumberNavigation)
                    .WithMany(p => p.PhoneNumberCollaborator)
                    .HasForeignKey(d => d.IdPhoneNumber)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhoneNumber_Collaborator_PhoneNumber");
            });

            modelBuilder.Entity<PositionMinistry>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(150)");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_State_Country");
            });
        }
    }
}