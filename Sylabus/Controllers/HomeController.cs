using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sylabus.Models;
using Sylabus.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace Sylabus.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    
    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [HttpPost("/register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (_context.Users.Any(u => u.Email == request.Email))
            return BadRequest(new { message = "E-mail już istnieje" });

        var hasher = new PasswordHasher<User>();
        var hashedPassword = hasher.HashPassword(null, request.Password);

        var user = new User
        {
            Name = request.FirstName,
            Surname = request.LastName,
            Email = request.Email,
            Password = hashedPassword
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new { message = "Witaj " + user.Name });
    }
    
    [HttpPost("/login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user == null)
            return Unauthorized("Nieprawidłowy e-mail lub hasło");

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.Password, request.Password);

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized("Nieprawidłowy e-mail lub hasło");

        // Zalogowanie użytkownika do sesji
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserName", user.Name);

        return Ok(new { message = "Zalogowano", userId = user.Id });
       
    }
}