﻿using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        //Need it to create a user for auth
        public async Task<Guid> CreateUser(string email, string password)
        {
            var existinguser = await _dbcontext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (existinguser != null)
            {

                throw new InvalidOperationException("пользователь с таким email уже существует.");
            };
            

            var user = new UserEntity()
            {
                UserId = Guid.NewGuid(),
                Email = email,
                Salt = Guid.NewGuid().ToString(),
                HashedPassword = password,


            };

            

            user.HashedPassword = _encryptor.HashPassword(user.HashedPassword, user.Salt);

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            return user.UserId;
        }

        public Guid GetByEmail(string email, string password)
        {

            var user =  _dbcontext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                throw new InvalidOperationException("Пользователь не найден.");
            }

            var hashedPassword = _encryptor.HashPassword(password, user.Salt);
            if (hashedPassword != user.HashedPassword)
            {
                throw new InvalidOperationException("Неправильный пароль.");
            }

            

            return user.UserId;
        }



    }
}
