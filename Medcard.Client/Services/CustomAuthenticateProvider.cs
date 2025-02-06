using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Medcard.Client.Services
{
    public class CustomAuthenticateProvider :AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        public CustomAuthenticateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
