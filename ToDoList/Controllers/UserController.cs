using ImageResizer.Plugins.Basic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.Account;
using SocialProgrammer.Service.Implementations;
using System.Security.Claims;
using SocialProgrammer.Service.Interfaces;

namespace SocialProgrammer.Controllers;

public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService) =>
        this.userService = userService;

    public IActionResult RegisterForm() => View();

    public IActionResult LoginForm() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var response = await userService.RegisterUser(registerViewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));

            return RedirectToAction("ArticleForm", "Article");
        }
        
        return View(registerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var response = await userService.LoginUser(loginViewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));

            return RedirectToAction("ArticleForm", "Article");
        }
        return View(loginViewModel);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("ArticleForm", "Article");
    }
}