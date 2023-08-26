using Microsoft.AspNetCore.Mvc;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.Article;
using SocialProgrammer.Service.Implementations;
using SocialProgrammer.Service.Interfaces;

namespace SocialProgrammer.Controllers;

public class ArticleController : Controller
{
    private readonly IArticleService articleService;
    private readonly IArticleRepository<ArticleEntity> articleRepository;

    public ArticleController(IArticleService articleService,
        IArticleRepository<ArticleEntity> articleRepository)
    {
        this.articleService = articleService;
        this.articleRepository = articleRepository; 
    }

    public async Task<IActionResult> ArticleForm()
    {
        var articles = await GetAll();
        return View(articles);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteArticle(string id)
    {
        
        var response = await articleService.DeleteArticle(id);
        return RedirectToAction("ArticleForm", "Article");
    }

    [HttpGet]
    public async Task<List<ArticleEntity>?> GetAll()
    {
        var response = await articleService.GetAll();
        return response.Data?.Where(x => x.Author == User.Identity.Name).ToList();
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticle(CreateArticleViewModel createArticleViewModel)
    {
        var response = await articleService.CreateArticle(createArticleViewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }
        return BadRequest(new { description = response.Description });
    }
}