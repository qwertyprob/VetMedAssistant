using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
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
        private readonly AppDbContext _dbcontext;
        public AuthRepository(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<Guid> CreateUser(string email, string password)
        {
            var user = new UserEntity()
            {
                UserId = Guid.NewGuid(),
                Email = email,
                HashedPassword = password,
                Salt = Guid.NewGuid().ToString()

            };

            _dbcontext.Users.Add(user);
            await _dbcontext.SaveChangesAsync();

            return user.UserId;
        }
    }
}
