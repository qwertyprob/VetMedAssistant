using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medcard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IMedcardRepository _medcardRepository;

        public OwnerController(IMedcardRepository medcardRepository)
        {
            _medcardRepository = medcardRepository;
        }

        // GET: api/Owner
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<OwnerDto>>> GetAllOwners()
        {
            var owners = await _medcardRepository.GetAllAsync();
            return Ok(owners);
        }

        // GET: api/Owner/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OwnerDto>> GetOwnerById(Guid id)
        {
            var owner = await _medcardRepository.GetByIdAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(owner);
        }

        // POST: api/Owner
        [HttpPost]
        public async Task<ActionResult<OwnerDto>> CreateOwner([FromBody] MedcardViewModel medcardViewModel)
        {
            if (medcardViewModel == null)
            {
                return BadRequest();
            }

            var createdOwner = await _medcardRepository.CreateAsync(medcardViewModel);
            return CreatedAtAction(nameof(GetOwnerById), new { id = createdOwner.Id }, createdOwner);
        }

        // PUT: api/Owner/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<OwnerDto>> UpdateOwner(Guid id, [FromBody] MedcardViewModel medcardViewModel)
        {
            if (medcardViewModel == null)
            {
                return BadRequest();
            }

            var updatedOwner = await _medcardRepository.UpdateAsync(id, medcardViewModel);
            if (updatedOwner == null)
            {
                return NotFound();
            }

            return Ok(updatedOwner);
        }

        // PATCH: api/Owner/{id}/drugs
        [HttpPatch("{id:guid}/drugs")]
        public async Task<ActionResult<OwnerDto>> UpdateDrugs(Guid id, [FromBody] string drugs)
        {
            var updatedOwner = await _medcardRepository.UpdateDrugsAsync(id, drugs);
            if (updatedOwner == null)
            {
                return NotFound();
            }

            return Ok(updatedOwner);
        }

        // PATCH: api/Owner/{id}/treatments
        [HttpPatch("{id:guid}/treatments")]
        public async Task<ActionResult<OwnerDto>> UpdateTreatments(Guid id, [FromBody] string treatments)
        {
            var updatedOwner = await _medcardRepository.UpdateTreatAsync(id, treatments);
            if (updatedOwner == null)
            {
                return NotFound();
            }

            return Ok(updatedOwner);
        }

        // PATCH: api/Owner/{id}/recommendations
        [HttpPatch("{id:guid}/recommendations")]
        public async Task<ActionResult<OwnerDto>> UpdateRecommendations(Guid id, [FromBody] string recommendations)
        {
            var updatedOwner = await _medcardRepository.UpdateRecAsync(id, recommendations);
            if (updatedOwner == null)
            {
                return NotFound();
            }

            return Ok(updatedOwner);
        }

        // DELETE: api/Owner/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteOwner(Guid id)
        {
            var isDeleted = await _medcardRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
