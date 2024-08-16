using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Dto;

namespace Medcard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedcardController : ControllerBase
    {
        public readonly IMedcardRepository medcardRepository;
        private readonly IMedcardService medcardService;

        public MedcardController(IMedcardRepository medcardRepository ,IMedcardService medcardService)
        { 
            this.medcardRepository = medcardRepository;
            this.medcardService = medcardService;
        }


        [HttpGet("GET")]

        public async Task<IActionResult> GetAllAsync()
        {
            var medcard = await medcardService.GetAllAsync();
            
            return new JsonResult(medcard);
        }

        [HttpGet("GET/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var medcard = await medcardService.GetByIdAsync(id);

            

            return Ok(medcard); 
        }


    }
}
