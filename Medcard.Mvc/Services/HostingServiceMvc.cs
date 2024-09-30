using Medcard.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Medcard.Mvc.Services
{
    public class HostingServiceMvc : IHostingServiceMvc
    {
        private readonly IConfiguration _configuration;
        

        public HostingServiceMvc(IConfiguration configuration)
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
