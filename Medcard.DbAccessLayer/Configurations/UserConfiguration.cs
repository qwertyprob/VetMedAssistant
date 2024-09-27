using Medcard.DbAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Medcard.DbAccessLayer.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            // Задаем имя таблицы
            builder.ToTable("Users");

            // Указываем первичный ключ
            builder.HasKey(u => u.UserId); 

            // Задаем длину для Email
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            // Задаем уникальный индекс для Email
            builder.HasIndex(u => u.Email)
                .IsUnique();

            // Конфигурация для остальных полей
            builder.Property(u => u.HashedPassword)
                .IsRequired();

            builder.Property(u => u.Salt)
                .IsRequired();
        }
    }


}
