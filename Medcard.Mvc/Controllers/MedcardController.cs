
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
using System.Globalization;

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

        [HttpPost]
        public  IActionResult SearchMedcardPost(string clientName)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                return RedirectToAction("Index"); // или "SearchMedcard" если хотите показать всех
            }

            return RedirectToAction("SearchMedcard", new { clientName });
        }

        [HttpGet("SearchMedcard/{clientName}")]
        public async Task<IActionResult> SearchMedcard(string clientName)
        {
            
<<<<<<< HEAD
            ViewBag.ClientName = clientName;
=======
            ViewBag.ClientName = clientName.Trim();
>>>>>>> refactoring
            

            var medcards = await _medcardService.GetAllFromSearchAsync(clientName.Trim());
            

            return View(medcards);
        }

        public async Task<IActionResult> Index()
        {
            var medcard = await _medcardService.GetAllAsync();



            return View("Index", medcard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedcardViewModel medcardViewModel)
        {
            
            

            if (ModelState.IsValid)
            {
                medcardViewModel.OwnerName = medcardViewModel.OwnerName?.Trim();
                medcardViewModel.PhoneNumber = medcardViewModel.PhoneNumber?.Trim();
                medcardViewModel.DateCreate = DateTime.UtcNow.ToLocalTime();
                medcardViewModel.PetName = medcardViewModel.PetName?.Trim();
                medcardViewModel.ChipNumber = medcardViewModel.ChipNumber?.Trim();
                medcardViewModel.Breed = medcardViewModel.Breed?.Trim();
                medcardViewModel.Drugs = "Здесь пока ничего нет!";
                medcardViewModel.Treatments = "Здесь пока ничего нет!";

                var medcard = await _medcardService.CreateAsync(medcardViewModel);

                return RedirectToAction("Index");
            }

            

            return View(medcardViewModel);
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
        public async Task<IActionResult> UpdateMedcard(Guid id, MedcardViewModel medcardViewModel)
        {
            
                medcardViewModel.OwnerName = medcardViewModel.OwnerName?.Trim();
                medcardViewModel.PhoneNumber = medcardViewModel.PhoneNumber?.Trim();
                medcardViewModel.DateCreate= DateTime.UtcNow.ToLocalTime();
                medcardViewModel.PetName = medcardViewModel.PetName?.Trim();
                medcardViewModel.ChipNumber = medcardViewModel.ChipNumber?.Trim();
                medcardViewModel.Breed = medcardViewModel.Breed?.Trim();
                medcardViewModel.Drugs = "Здесь пока ничего нет!";
                medcardViewModel.Treatments = "Здесь пока ничего нет!";
              
            

            var updatedMedcard = await _medcardService.UpdateAsync(id, medcardViewModel);
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
        public async Task<IActionResult> Search(string name)
        {
            var owner = await _medcardService.SearchByNameAsync(name);

            if (owner == Guid.Empty)
            {
                
                return RedirectToAction("Index", "Medcard");
            }
            if(name == null)
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
