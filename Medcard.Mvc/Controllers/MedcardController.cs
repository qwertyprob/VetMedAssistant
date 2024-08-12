using Medcard.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Medcard.Mvc.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

public class MedcardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
