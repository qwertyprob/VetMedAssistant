using System;
using System.Threading.Tasks;

namespace Medcard.Mvc.Services
{
    public interface IAuthServiceMvc
    {
        Task<Guid> CreateUser(string email, string password);
    }
}