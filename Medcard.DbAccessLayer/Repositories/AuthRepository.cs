using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IEncrypt _encryptor;
        private readonly AppDbContext _dbcontext;
        public AuthRepository(AppDbContext dbContext, IEncrypt ecryptor)
        {
            _dbcontext = dbContext;
            _encryptor = ecryptor;
        }

        public async Task<Guid>CreateUser(string email, string password)
        {
            var user = new UserEntity()
            {
                UserId = Guid.NewGuid(),
                Email = email,
                Salt = Guid.NewGuid().ToString(),
                HashedPassword= password,
                

            };

            user.HashedPassword = _encryptor.HashPassword(user.HashedPassword, user.Salt);

            
            

            // Проверка существующего email
            var existingUser = await _dbcontext.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                // Обработка случая, когда email уже существует
                throw new InvalidOperationException("Пользователь с таким email уже существует.");
            }

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            return user.UserId;
        }
    }
}
