using Medcard.DbAccessLayer.Interfaces;
using System.Threading.Tasks;
using System;

namespace Medcard.Mvc.Services
{
    public class AuthServiceMvc : IAuthServiceMvc
    {
        private readonly IAuthRepository _authRepository;
        public AuthServiceMvc(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<Guid> CreateUser(string email, string password)
        {
            return await _authRepository.CreateUser(email, password);

        }

        public string Login(string email, string password)
        {
            return _authRepository.Login(email, password);
        }
    }
}
