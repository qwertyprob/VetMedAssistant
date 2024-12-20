﻿
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Medcard.DbAccessLayer.Interfaces;
using System.Linq;
using Microsoft.Extensions.Logging;
using Medcard.DbAccessLayer.Dto;
using Medcard.Mvc.Models;
using Medcard.DbAccessLayer.Entities;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Medcard.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Medcard.Mvc.Abstractions;

namespace Medcard.Mvc.Controllers
{
    //[AuthorizeRole("Admin")]
    public class MedcardController : Controller
    {
        private readonly IMedcardServiceMvc _medcardService;

        private readonly ILogger<MedcardController> _logger;


        public MedcardController(IMedcardServiceMvc medcardServiceMvc, ILogger<MedcardController> logger)
        {
            _medcardService = medcardServiceMvc;
            _logger = logger;



        }
        [Route("/Print")]
        public IActionResult Print()
        {
            return View();
        }

            [Route("/")]
        public async Task<IActionResult> Index()
        {

            var userRole = HttpContext.Session.GetString("UserRole");


            var medcard = await _medcardService.GetAllAsync();

            return View("Index", medcard);
        }

        [HttpPost]
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
                medcardViewModel.Drugs = "\n-\n-\n-\n-\n-\n-";
                medcardViewModel.Treatments = "\n-\n-\n-\n-\n-\n-";
                medcardViewModel.Recomendations = "\n-\n-\n-\n-\n-\n-";

                var medcard = await _medcardService.CreateAsync(medcardViewModel);

                return Redirect("/");
            }

            

            return View(medcardViewModel);
        }
        [HttpPost]
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
        [Route("/Get/{id?}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var medcard = await _medcardService.GetByIdAsync(id);

            if (medcard is null)
            {
                return View("NotFound"); 
            }

            return View("More", medcard);
        }


        [HttpGet]
        [Route("/Update/{id}")]
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
        [Route("/Update/{id}")]
        public async Task<IActionResult> UpdateMedcard(Guid id, MedcardViewModel medcardViewModel)
        {
            

                medcardViewModel.OwnerName = medcardViewModel.OwnerName?.Trim();
                medcardViewModel.PhoneNumber = medcardViewModel.PhoneNumber?.Trim();
                medcardViewModel.DateCreate= DateTime.UtcNow.ToLocalTime();
                medcardViewModel.PetName = medcardViewModel.PetName?.Trim();
                medcardViewModel.ChipNumber = medcardViewModel.ChipNumber?.Trim();
                medcardViewModel.Breed = medcardViewModel.Breed?.Trim();




            var updatedMedcard = await _medcardService.UpdateAsync(id, medcardViewModel);
            if (updatedMedcard == null)
            {
                return NotFound();
            }
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDrugsAndTreatments(Guid id, string Drugs, string Treatments, string Recomendations, string Action, Guid PetId)
        {
            // Проверка на валидность модели
            if (!ModelState.IsValid)
            {
                return View("More", await _medcardService.GetByIdAsync(id));
            }

            Drugs = Drugs ?? " ";
            Treatments = Treatments ?? " ";
            Recomendations = Recomendations ?? " ";



            // Обработка действий
            switch (Action)
            {
                case "UpdateDrugs":
                    await _medcardService.UpdateDrugsAsync(PetId, Drugs);
                    break;
                case "UpdateTreatments":
                    await _medcardService.UpdateTreatmentsAsync(PetId, Treatments);
                    break;
                case "UpdateRecomendations":
                    await _medcardService.UpdateRecomendAsync(PetId, Recomendations);
                    break;
                default:
                    ModelState.AddModelError(string.Empty, "Неизвестное действие.");
                    return View("More", await _medcardService.GetByIdAsync(id));
            }

            // Перенаправление после успешного обновления
            return RedirectToAction(nameof(GetById), new { id });
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
