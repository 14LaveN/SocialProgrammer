using Microsoft.AspNetCore.Mvc;
using SocialProgrammer.Service.Interfaces;

namespace SocialProgrammer.Controllers;

public class BestArticlesController : Controller
{
    private readonly IArticleService articleService;
    public BestArticlesController(IArticleService articleService) 
    {
        this.articleService = articleService;
    }   

    public async Task<IActionResult> BestArticlesForm()
    {
        var articles = await articleService.GetAll();
        return View(articles.Data);
    }
}
