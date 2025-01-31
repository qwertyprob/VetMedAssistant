using Medcard.Bl.Abstraction;
using Medcard.Bl.Jwt;
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
        private readonly IJwtProvider _jwt;
        

        public AuthService(IAuthRepository authRepository, IJwtProvider jwt)
        {
            _authRepository = authRepository;
            _jwt = jwt;


        }

        public async Task<string> Register(string email, string password)
        {

            var userId =  await _authRepository.CreateUser(email, password);

            return userId.ToString();

        }

        public string Login(string email, string password)
        {
            var userId = _authRepository.GetByEmail(email, password);

            var token = _jwt.GenerateToken(userId.ToString());
            
            return token;
        }
        

        
    }
}
