using Medcard.Api.Jwt;
using Medcard.Bl.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IJwtProvider = Medcard.Bl.Abstraction.IJwtProvider;

namespace Medcard.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IJwtProvider _jwt;

        public AuthenticationController(IAuthService service, IJwtProvider jwt)
        {
            _service = service;
            _jwt = jwt;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            
                var user =  _service.Login(username, password);
            if (user == null)
            {
                return NotFound();
            }

                var token = _jwt.GenerateToken(user);

                return Ok(new { Token = token });
            
            
        }



        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = await _service.Register(email, password);

  
            return Ok(user);




        }
    }
}
