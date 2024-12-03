namespace Medcard.Api.Jwt
{
    public interface IJwtProvider
    {
        string GenerateToken(string id);
    }
}