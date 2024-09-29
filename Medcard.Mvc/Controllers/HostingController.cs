using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Medcard.Mvc.Controllers
{
    public class HostingController : Controller
    {
        private readonly IConfiguration _configuration;

        public HostingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //Финт ушами
        [HttpGet]
        public string GetEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable("GlobalMedcardRender") ?? _configuration["GlobalMedcardRender"];
        }

    }
}
