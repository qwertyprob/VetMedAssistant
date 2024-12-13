namespace Medcard.Bl.Abstraction
{
    public interface IAuthService
    {
        Task<Guid> Register(string email, string password);
        string Login(string email, string password);
    }
}