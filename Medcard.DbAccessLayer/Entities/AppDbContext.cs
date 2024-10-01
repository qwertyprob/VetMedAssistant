using Medcard.DbAccessLayer.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace Medcard.DbAccessLayer.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<OwnerEntity> Owners { get; set; }
        public DbSet<PetEntity> Pets { get; set; }
        public DbSet<DrugEntity> Drugs { get; set; }
        public DbSet<TreatmentEntity> Treatments { get; set; }
        public DbSet<RecomendationEntity> Recomendations { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }


    }
}
