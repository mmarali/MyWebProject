using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sylabus.Models;
using Sylabus.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace Sylabus.Controllers;

public class SylabusController : Controller
{
    private readonly ILogger<SylabusController> _logger;
    private readonly AppDbContext _context;
    
    public SylabusController(ILogger<SylabusController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/sylabus")]
    public IActionResult Sylabus()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Index", "Home"); // lub zwróć 401
        }

        ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        return View();
    }
    
    [HttpGet("/dodaj-sylabus")]
    public IActionResult AddSylabus()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Index", "Home"); // lub zwróć 401
        }

        ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        return View();
    }
}