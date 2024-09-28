using System;
using System.Threading.Tasks;

namespace Medcard.DbAccessLayer.Interfaces
{
    public interface IAuthRepository
    {
        Task<Guid> CreateUser(string email, string password);
        string Login(string email, string password);
        bool IsLoggedIn();
    }
}