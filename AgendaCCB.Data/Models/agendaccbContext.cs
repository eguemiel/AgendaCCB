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
        public virtual DbSet<PositionMinistry> PositionMinistry { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
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
                    .HasForeignKey<CommonCongregation>(d => d.Id)
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

                entity.HasOne(d => d.IdCollaboradorNavigation)
                    .WithMany(p => p.PhoneNumber)
                    .HasForeignKey(d => d.IdCollaborador)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PhoneNumber_Collaborador");
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

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("0");

                entity.Property(e => e.AnswerPassword).HasColumnType("varchar(150)");

                entity.Property(e => e.Blocked).HasDefaultValueSql("0");

                entity.Property(e => e.CountFailurePasswordFailure).HasDefaultValueSql("0");

                entity.Property(e => e.CountFailureResponseFailure).HasDefaultValueSql("0");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.LastAccess).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.QuestionPassword).HasColumnType("varchar(150)");
            });
        }
    }
}