using Microsoft.AspNetCore.Mvc;

namespace SocialProgrammer.Controllers;

public class PhotoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile photo)
    {
        if (photo != null)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", photo.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(stream);

                ViewBag.Message = "Фотография успешно загружена.";
                ViewBag.PhotoPath = "/photos/" + photo.FileName;

                return View();
            }
        }
        return View();
    }
}