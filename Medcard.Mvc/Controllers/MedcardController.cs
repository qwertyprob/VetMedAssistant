
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.Mvc.Services;
using System.Linq;
using Microsoft.Extensions.Logging;
using Medcard.DbAccessLayer.Dto;
using Medcard.Mvc.Models;
using Medcard.DbAccessLayer.Entities;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Medcard.Mvc.Controllers
{
    public class MedcardController : Controller
    {
        private readonly IMedcardServiceMvc _medcardService;
        private readonly ILogger<MedcardController> _logger;


        public MedcardController(IMedcardServiceMvc medcardServiceMvc, ILogger<MedcardController> logger)
        {
            _medcardService = medcardServiceMvc;
            _logger = logger;

        }



        public async Task<IActionResult> Index()
        {
            var medcard = await _medcardService.GetAllAsync();



            return View("Index", medcard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedcardViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Drugs = "Здесь пока ничего нет!";
                model.Treatments = "Здесь пока ничего нет!";

                var medcard = await _medcardService.CreateAsync(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _medcardService.DeleteAsync(id);

            if (isDeleted)
            {
                return RedirectToAction("Index");
            }


            return View("Error");

        }
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var medcard = await _medcardService.GetByIdAsync(id);

            if(medcard is null)
            {
                return View("NotFound");
            }

            return View("More", medcard);
        }
        public IActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        [Route("Medcard/Update/{id}")]
        public async Task<IActionResult> UpdateMedcard(Guid id)
        {
            var medcard = await _medcardService.GetByIdAsync(id);
            if (medcard == null)
            {
                return NotFound();
            }
            return View("UpdateMedcard", medcard);
        }

        [HttpPost]
        [Route("Medcard/Update/{id}")]
        public async Task<IActionResult> UpdateMedcard(Guid id, MedcardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Drugs = "Здесь пока ничего нет!";
                model.Treatments = "Здесь пока ничего нет!";
                return View("UpdateMedcard", model);
            }

            var updatedMedcard = await _medcardService.UpdateAsync(id, model);
            if (updatedMedcard == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", updatedMedcard);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDrugsAndTreatments(Guid id, string Drugs, string Treatments, string Action, Guid PetId) 
        {
            
            if (!ModelState.IsValid)
            {
                
                return View("More", await _medcardService.GetByIdAsync(id)); 
            }
            if (Drugs is null || Treatments is null)
            {
                return RedirectToAction(nameof(GetById), new { id });
            }

            if (Action == "UpdateDrugs")
            {
                await _medcardService.UpdateDrugsAsync(PetId, Drugs);
            }
            else if (Action == "UpdateTreatments")
            {
                await _medcardService.UpdateTreatmentsAsync(PetId, Treatments);
            }

           


            return RedirectToAction(nameof(GetById), new { id });
        }


        [HttpPost]
        public async Task<IActionResult> Search(string petName)
        {
            var owner = await _medcardService.SearchByPetName(petName);

            if (owner == Guid.Empty)
            {
                
                return RedirectToAction("Index", "Medcard");
            }
            if(petName == null)
                return View("NotFound");
        
            

            return RedirectToAction("GetById", "Medcard", new { id = owner });
        }

        public IActionResult More(Guid id)
        {
            var model = _medcardService.GetByIdAsync(id); 
            if (model == null)
            {
                return NotFound(); 
            }
            return View(model);
        }


    }
}
