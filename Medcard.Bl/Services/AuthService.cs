using Medcard.Bl.Abstraction;
using Medcard.DbAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Bl.Services
{
    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepository;
        

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            
        }

        public async Task<Guid> Register(string email, string password)
        {

            return await _authRepository.CreateUser(email, password);

        }

        public string Login(string email, string password)
        {
            var userId = _authRepository.GetByEmail(email, password);
            
            return userId.ToString();
        }
        

        
    }
}
