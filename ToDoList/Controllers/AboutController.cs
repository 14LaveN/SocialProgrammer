using Microsoft.AspNetCore.Mvc;

namespace SocialProgrammer.Controllers;

public class AboutController : Controller
{
    public IActionResult AboutForm() => View();
}