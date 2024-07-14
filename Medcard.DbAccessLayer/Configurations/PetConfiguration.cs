using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medcard.DbAccessLayer.Entities;


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
        }
    }
}
