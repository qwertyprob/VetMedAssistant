using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Dto;
using Microsoft.AspNetCore.Diagnostics;
using System.IO;

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

            if(medcard is null)
                return BadRequest("No Medcards!");

            
            return Ok(medcard);
        }


        [HttpGet("GET/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
           
            var medcard = await medcardService.GetByIdAsync(id);

            
            if (medcard == null)
            {
                return NotFound(); 
            }

          
            return Ok(medcard);
        }
        [HttpPost("CREATE")]
        public async Task<IActionResult> CreateAsync(MedcardViewModel medcardViewModel)
        {
            var medcard = await medcardService.CreateAsync(medcardViewModel);

            return Ok(medcard);

        }
        [HttpPut("UPDATE/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] MedcardViewModel medcardViewModel)
        {
            var medcard = await medcardService.UpdateAsync(id,medcardViewModel);

            if (medcard == null)
            {
                return NotFound();
            }

            return Ok(medcard);
        }

        [HttpDelete("DELETE/{id}")]
        public async Task <IActionResult> DeleteAsync(Guid id)
        {
            var medcard = await medcardService.DeleteAsync(id);

            if (medcard == null)
            {
                return NotFound();
            }
            return Ok(medcard);

        }

    }
}
