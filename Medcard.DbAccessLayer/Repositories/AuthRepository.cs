using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor  _httpContext;
        public AuthRepository(AppDbContext dbContext, IEncrypt ecryptor, IHttpContextAccessor httpContexter)
        {
            _dbcontext = dbContext;
            _encryptor = ecryptor;
            _httpContext= httpContexter;
        }

        public async Task<Guid> CreateUser(string email, string password)
        {
            var user = new UserEntity()
            {
                UserId = Guid.NewGuid(),
                Email = email,
                Salt = Guid.NewGuid().ToString(),
                HashedPassword = password,


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

        public string Login(string email, string password)
        {
            // Поиск пользователя по email
            var user = _dbcontext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                throw new InvalidOperationException("Пользователь не найден.");
            }

            // Проверка пароля
            var hashedPassword = _encryptor.HashPassword(password, user.Salt);
            if (hashedPassword != user.HashedPassword)
            {
                throw new InvalidOperationException("Неправильный пароль.");
            }

            // Установка UserId в сессию
            _httpContext.HttpContext?.Session.Set("userid",user.UserId.ToByteArray());

            return user.UserId.ToString(); 
        }

    }
}
