
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medcard.DbAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Medcard.DbAccessLayer.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<PetEntity>
    {
        public void Configure(EntityTypeBuilder<PetEntity> builder)
        {
            // Настройка связи один ко многим между Pet и Treatment
            builder
                .HasMany(p => p.Treatments)
                .WithOne(t => t.Pet)
                .HasForeignKey(t => t.PetId);

            // Настройка связи один ко многим между Pet и Drug
            builder
                .HasMany(p => p.Drugs)
                .WithOne(d => d.Pet)
                .HasForeignKey(d => d.PetId);
            // Настройка связи один ко многим между Pet и Recomendation
            builder
                .HasMany(p => p.Recomendations)
                .WithOne(t => t.Pet)
                .HasForeignKey(t => t.PetId);
            // Настройка связи один ко многим между Pet и Tests
            builder
                .HasMany(p => p.Tests)
                .WithOne(t => t.Pet)
                .HasForeignKey(t => t.PetId);
        }
    }
}
