namespace Medcard.Bl.Abstraction
{
    public interface IAuthService
    {
        Task<string> Register(string email, string password);
        string Login(string email, string password);
    }
}