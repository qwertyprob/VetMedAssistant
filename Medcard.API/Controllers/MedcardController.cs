using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Medcard.Core.Interfaces;
using Medcard.DbAccessLayer.Entities;

namespace Medcard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedcardController : ControllerBase
    {
        public readonly IMedcardRepository<OwnerEntity, PetEntity, DrugEntity, TreatmentEntity> medcardRepository;
        public MedcardController(IMedcardRepository<OwnerEntity, PetEntity, DrugEntity, TreatmentEntity> medcardRepository)
        { 
            this.medcardRepository = medcardRepository; 
        }


        [HttpGet("GET")]

        public async Task<IActionResult> GetAll()
        {
            var medcard = await medcardRepository.GetAllAsync();
            
            return new JsonResult(medcard);
        }



    }
}
