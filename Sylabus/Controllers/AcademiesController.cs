using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sylabus.Models;
using Sylabus.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace Sylabus.Controllers;

public class AcademiesController : Controller
{
    private readonly ILogger<AcademiesController> _logger;
    private readonly AppDbContext _context;
    
    public AcademiesController(ILogger<AcademiesController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/uczelnie")]
    public IActionResult Academies()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Index", "Home"); // lub zwróć 401
        }

        ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        return View();
    }
    
    [HttpGet("/api/academies")]
    public IActionResult GetAcademies()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return Unauthorized();
        }

        var academies = _context.Academies.ToList();
        return Json(academies);
    }
}