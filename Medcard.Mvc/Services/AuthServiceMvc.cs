using Medcard.DbAccessLayer.Interfaces;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Medcard.Mvc.Abstractions;

namespace Medcard.Mvc.Services
{
    public class AuthServiceMvc : IAuthServiceMvc
    {
        private const string Role = "Admin";
        private readonly IAuthRepository _authRepository;
        private readonly IHttpContextAccessor _httpContext;
        public AuthServiceMvc(IAuthRepository authRepository, IHttpContextAccessor httpContext)
        {
            _authRepository = authRepository;
            _httpContext = httpContext;
        }

        public async Task<Guid> CreateUser(string email, string password)
        {


            return await _authRepository.CreateUser(email, password);

        }

        public string Login(string email, string password)
        {
            //Set role for authorization

            return _authRepository.Login(email, password);
        }
        private bool IsLoggedIn()
        {

            var session = _httpContext.HttpContext?.Session;


            if (session != null && session.TryGetValue("userid", out _))
            {
                return true;
            }

            return false;
        }

        public void SetRoleSession()
        {
            if (IsLoggedIn())
            {
                _httpContext.HttpContext!.Session.SetString("userRole", Role);
            }


        }
    }
}
