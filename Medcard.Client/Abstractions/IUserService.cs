using Medcard.Client.Models;

namespace Medcard.Client.Services
{
    public interface IUserService
    {
        Task<string> Login(LoginViewModel model);
    }
}