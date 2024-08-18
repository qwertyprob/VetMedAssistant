using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Dto;


namespace Medcard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
        
        
        private readonly IMedcardService medcardService;

        public APIController(IMedcardService medcardService)
        { 
            
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
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await medcardService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent(); 
        }



    }
}
