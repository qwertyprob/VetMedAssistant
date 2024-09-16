using Medcard.DbAccessLayer.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Medcard.DbAccessLayer.Entities
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<OwnerEntity> Owners { get; set; }
        public DbSet<PetEntity> Pets { get; set; }
        public DbSet<DrugEntity> Drugs { get; set; }
        public DbSet<TreatmentEntity> Treatments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            base.OnModelCreating(modelBuilder);
        }


    }
}
