namespace Medcard.Bl.Abstraction
{
    public interface IJwtProvider
    {
        string GenerateToken(string id);
    }
}