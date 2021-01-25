using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TamagotchiUI.Models
{
    public partial class TamagotchiContext : DbContext
    {
        public TamagotchiContext()
        {
        }

        public TamagotchiContext(DbContextOptions<TamagotchiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivitiesCategory> ActivitiesCategories { get; set; }
        public virtual DbSet<ActivitiesHistory> ActivitiesHistories { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<HealthStatus> HealthStatuses { get; set; }
        public virtual DbSet<LifeCycleStatus> LifeCycleStatuses { get; set; }
        public virtual DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server = localhost\\SQLEXPRESS; Database=TamagotchiDB;Trusted_Connection = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivitiesCategory>(entity =>
            {
                entity.HasKey(e => e.AcitivtyCategoryId)
                    .HasName("PK__Activiti__3A7A4E9C6067E761");
            });

            modelBuilder.Entity<ActivitiesHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PK__Activiti__4D7B4ADD975BD024");

                entity.Property(e => e.ActivityDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivitiesHistories)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK_ActivityHistoryActivity");

                entity.HasOne(d => d.AnimalCycleStatus)
                    .WithMany(p => p.ActivitiesHistories)
                    .HasForeignKey(d => d.AnimalCycleStatusId)
                    .HasConstraintName("FK_ActivityHistoryCycle");

                entity.HasOne(d => d.AnimalHealthStatus)
                    .WithMany(p => p.ActivitiesHistories)
                    .HasForeignKey(d => d.AnimalHealthStatusId)
                    .HasConstraintName("FK_ActivityHistoryHealthStatus");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.ActivitiesHistories)
                    .HasForeignKey(d => d.AnimalId)
                    .HasConstraintName("FK_ActivityHistoryAnimal");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasOne(d => d.ActivityCategory)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.ActivityCategoryId)
                    .HasConstraintName("FK_ActivityCategory");
            });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.Property(e => e.Acleanliness).HasDefaultValueSql("((50))");

                entity.Property(e => e.Ahappiness).HasDefaultValueSql("((50))");

                entity.Property(e => e.Ahunger).HasDefaultValueSql("((50))");

                entity.Property(e => e.Aweight).HasDefaultValueSql("((4))");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HealthStatusId).HasDefaultValueSql("((1))");

                entity.Property(e => e.LifeCycleId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.HealthStatus)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.HealthStatusId)
                    .HasConstraintName("FK_AnimalHealthStatus");

                entity.HasOne(d => d.LifeCycle)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.LifeCycleId)
                    .HasConstraintName("FK_AnimalLifeCycleStatus");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_AnimalPlayer");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasIndex(e => e.ActiveAnimalId, "idx_playeractiveanimalid")
                    .IsUnique()
                    .HasFilter("([ActiveAnimalID] IS NOT NULL)");

                entity.HasOne(d => d.ActiveAnimal)
                    .WithOne(p => p.PlayerNavigation)
                    .HasForeignKey<Player>(d => d.ActiveAnimalId)
                    .HasConstraintName("FK_ActiveAnimalID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
