using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PensionManager.PensionManger.Domain;

namespace PensionManager.PensionManager.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User>
{
    // DbSets for custom entities
        public DbSet<Employer> Employers { get; set; }
        public DbSet<PensionPlan> PensionPlans { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contribution> Contributions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Required for Identity configurations

            // Configure Address as an owned entity for ApplicationUser
            modelBuilder.Entity<User>(entity =>
            {
                entity.OwnsOne(u => u.Address, address =>
                {
                    address.Property(a => a.Street).HasColumnName("Street");
                    address.Property(a => a.City).HasColumnName("City");
                    address.Property(a => a.State).HasColumnName("State");
                    address.Property(a => a.PostalCode).HasColumnName("PostalCode");
                    address.Property(a => a.Country).HasColumnName("Country");
                });

                // // Relationships
                // entity.HasOne(u => u.Employer)
                //       .WithMany(e => e.User)
                //       .HasForeignKey(u => u.EmployerId)
                //       .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(u => u.PensionPlan)
                      .WithMany(p => p.Users)
                      .HasForeignKey(u => u.PensionPlanId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(u => u.NationalId).IsUnique();
            });

            // Configure Employer
            modelBuilder.Entity<Employer>(entity =>
            {
                entity.HasKey(e => e.EmployerId);
                entity.OwnsOne(e => e.Address, address =>
                {
                    address.Property(a => a.Street).HasColumnName("Street");
                    address.Property(a => a.City).HasColumnName("City");
                    address.Property(a => a.State).HasColumnName("State");
                    address.Property(a => a.PostalCode).HasColumnName("PostalCode");
                    address.Property(a => a.Country).HasColumnName("Country");
                });
            });

            // Configure PensionPlan
            modelBuilder.Entity<PensionPlan>(entity =>
            {
                entity.HasKey(p => p.PensionPlanId);
            });
        }
}
