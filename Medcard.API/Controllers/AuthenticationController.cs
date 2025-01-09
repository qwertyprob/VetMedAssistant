using Medcard.Bl.Jwt;
using Medcard.Bl.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medcard.Api.Controllers
{ 
    [ApiController]
    [Route("/api/authorization/")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtProvider _jwt;
        public AuthenticationController(IAuthService authService, IJwtProvider jwt) 
        {
            _authService = authService;
            _jwt = jwt;
        }
        [HttpPost("Login")]
        public IActionResult Login(string login, string password)
        {
            var result = _authService.Login(login, password);
            
            var token = _jwt.GenerateToken(result);

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string login, string password)
        {

            var userId = await _authService.Register(login, password);
            var token = _jwt.GenerateToken(userId.ToString());

            return Ok(userId);
        }


    }
}
