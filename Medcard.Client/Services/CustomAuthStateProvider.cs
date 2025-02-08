using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Server;
using Blazored.SessionStorage;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;

namespace Medcard.Client.Services
{

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly ILocalStorageService _localStorage;
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

        public CustomAuthStateProvider(ISessionStorageService sessionStorage, ILocalStorageService localStorageService)
        {
            _sessionStorage = sessionStorage;
            _localStorage = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var tokenResult = await _localStorage.GetItemAsync<string>("authToken");

                if (string.IsNullOrEmpty(tokenResult))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); // Неавторизованный
                }

                var claims = ParseClaimsFromJwt(tokenResult);
                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                _currentUser = user;
                return new AuthenticationState(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении состояния аутентификации: {ex.Message}");
            }

            // Всегда возвращаем пустого пользователя вместо null
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }


        public async Task Login(string token)
        {
            try
            {
                await _localStorage.SetItemAsync<string>("authToken", token);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            _currentUser = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

       

        private IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            return jwt.Claims;
        }
    }

}
