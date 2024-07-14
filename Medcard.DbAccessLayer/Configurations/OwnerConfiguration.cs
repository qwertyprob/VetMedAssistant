using Medcard.DbAccessLayer.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<OwnerEntity>
    {
        public void Configure(EntityTypeBuilder<OwnerEntity> builder)
        {
            //настройка связи один ко многим 
            builder
           .HasMany(o => o.Pets)
           .WithOne(p => p.Owner)
           .HasForeignKey(p => p.OwnerId);

        }
    }
}
