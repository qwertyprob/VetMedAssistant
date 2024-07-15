using Medcard.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Medcard.Mvc.Models;

public class MedcardController : Controller
{
    private readonly IMedcardService _medcardService;

    public MedcardController(IMedcardService medcardService)
    {
        _medcardService = medcardService;
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
    public async Task<IActionResult> UpdateMedcard(
    Guid id, 
    string name, string phone,
    string petName, int chipNumber,
    int petAge, string petBreed,
    string petDrugs, string petTreatment)
    {
        var ownerModel = await _medcardService.UpdateMedcardAsync(id, name, phone, petName, chipNumber, petAge, petBreed, petDrugs, petTreatment);

        
        return View("Index"); 
    }
    [HttpPost]
    public async Task<IActionResult> CreateMedcard(
        string name, string phone,
        string petName, int chipNumber,
        int petAge, string petBreed)
    {
        string petDrugs = "Здесь пока пусто!"; 
        string petTreatment = "Здесь пока пусто!"; 

        var medcard = await _medcardService.CreateMedcardAsync(name, phone, petName, chipNumber, petAge, petBreed, petDrugs, petTreatment);
        var allMedcards = await _medcardService.GetMedcardsAsync(); 

        return View("MedcardList", allMedcards); 
    }
}
