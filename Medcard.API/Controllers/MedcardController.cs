using Medcard.Bl.Abstraction;
using Medcard.Bl.Models;
using Medcard.DbAccessLayer.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Medcard.Api.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("/api/")]
    public class MedcardController : ControllerBase
    {
        private readonly IMedcardService _service;

        public MedcardController(IMedcardService service) 
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateMedcard([FromBody]MedcardViewModel model)
        {
            var medcard = await _service.CreateAsync(model);


            return Ok(medcard);   
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetMedcards()
        {
            var medcard = await _service.GetAllAsync();

            return Ok(medcard);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetMedcardById(Guid id)
        {
            var medcard = await _service.GetByIdAsync(id);
            return Ok(medcard);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateMedcard(Guid id , MedcardViewModel model)
        {   
            var medcard = await _service.UpdateAsync(id , model);
            
            return Ok(medcard);
            
        }
        [HttpPut]
        [Route("tests/{id}")]
        public async Task<IActionResult> UpdateTests(Guid id, string text)
        {
            var medcard = await _service.UpdateTestsAsync(id, text);

            return Ok(medcard);
        }

        [HttpPut]
        [Route("treatments/{id}")]
        public async Task<IActionResult> UpdateTreat(Guid id, string text)
        {
            var medcard = await _service.UpdateTreatAsync(id, text);

            return Ok(medcard);
        }


        [HttpPut]
        [Route("drugs/{id}")]
        public async Task<IActionResult> UpdateDrugs(Guid id, string text)
        {
            var medcard = await _service.UpdateDrugsAsync(id, text);

            return Ok(medcard);
        }

        [HttpPut]
        [Route("recomendations/{id}")]
        public async Task<IActionResult> UpdateRecomendations(Guid id, string text)
        {
            var medcard = await _service.UpdateRecAsync(id, text);

            return Ok(medcard);

            
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteMedcard(Guid id)
        {

            var medcard = await _service.DeleteAsync(id);

            if (medcard)
            {
                return Ok(medcard);
            }

            return NotFound();


        }








    }
}
