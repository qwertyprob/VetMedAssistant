using Medcard.Bl.Abstraction;
using Medcard.Bl.Models;
using Medcard.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;

namespace Medcard.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly AppDbContext _context;


        public UserController(IAuthService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            try
            {
                var token = _service.Login(user.Email, user.Password);

                Response.Cookies.Append("Jwt", token);

                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(LoginViewModel user)
        {
            try
            {
                var userid = await _service.Register(user.Email, user.Password);



                return Ok("Успешная регистрация");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete]
        [Route("deleteUser")]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                var userByEmail = await _context.Users
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Email ==  email);

                if(userByEmail is not null)
                {
                    _context.Users.Remove(userByEmail);
                    await _context.SaveChangesAsync();

                    return Ok($"Успешно удалили юзера с мейлом {email}");
                }
                    
                return BadRequest("Такого эмейла не существует!");


                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
