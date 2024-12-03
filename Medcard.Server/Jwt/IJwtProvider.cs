namespace Medcard.Server.Jwt
{
    public interface IJwtProvider
    {
        string GenerateToken(string id);
    }
}