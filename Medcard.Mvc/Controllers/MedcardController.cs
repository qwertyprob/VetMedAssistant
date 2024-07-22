using Medcard.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Medcard.Mvc.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

public class MedcardController : Controller
{
    private readonly IMedcardService _medcardService;
    private ILogger<MedcardController> _logger;


    public MedcardController(IMedcardService medcardService , ILogger<MedcardController> logger)
    {
        _medcardService = medcardService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMedcardForm(MedcardUpdateModel model)
    {

        return View( model);

    }
    [HttpGet]
    [Route("Medcard/UpdateMedcard/{ownerId}")]
    public async Task<IActionResult> UpdateMedcard(Guid ownerId)
    {
        var ownerModel = await _medcardService.GetMedcardById(ownerId);

        if (ownerModel == null)
        {
            return NotFound(); 
        }

        return View("UpdateMedcard", ownerModel);

    }
    [HttpPost]
    public async Task<IActionResult> DeleteMedcard(Guid id)
    {
        await _medcardService.DeleteMedcardAsync(id);

        return RedirectToAction("Index", "Medcard");
    }
    [HttpGet]
    public async Task<IActionResult> More(Guid ownerId)
    {
        var ownerModel = await _medcardService.GetMedcardById(ownerId);

        return View(ownerModel);
    }

    public async Task<IActionResult> Index()
    {
        var ownerModels = await _medcardService.GetMedcardsAsync();

        return View(ownerModels);
    }

   

    [HttpPost]
    public async Task<IActionResult> CreateMedcard(MedcardCreateModel model)
    {
        var ownerModel = await _medcardService.CreateMedcardAsync(
            model.Name, model.Phone,
            model.PetName, model.ChipNumber,
            model.PetAge, model.PetBreed,
            model.PetDrugs, model.PetTreatment);



        return RedirectToAction("Index","Medcard");
    }
}
