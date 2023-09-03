using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Enum;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Search;
using ILogger = NLog.ILogger;

namespace SocialProgrammer.Controllers;

public class SearchController : Controller
{
    private readonly ISearchRepository<SearchEntity> searchRepository;
    private readonly IArticleRepository<ArticleEntity> articleRepository;
    private readonly IProfileRepository<ProfileEntity> profileRepository;
    
    public SearchController(ISearchRepository<SearchEntity> searchRepository,
        IArticleRepository<ArticleEntity> articleRepository,
        IProfileRepository<ProfileEntity> profileRepository)
    {
  
        this.searchRepository = searchRepository;
        this.articleRepository = articleRepository;
        this.profileRepository = profileRepository;
    }

    [HttpGet]
    public IActionResult SearchForm()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SearchArticlesForm(SearchResultViewModel searchResultViewModel)
    {
        var result = await GetResultByArticles(searchResultViewModel.Description);
        return View(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> SearchProfilesForm(SearchResultViewModel searchResultViewModel)
    {
        var result = await GetResultByProfiles(searchResultViewModel.Description);
        return View(result);
    }

    [HttpPost]
    public async Task<List<ArticleEntity>> GetResultByArticles(string searchLine)
    {
        var responseByArticles = await GetResultAfterSearchByArticles(searchLine);
        
        if (responseByArticles.StatusCode == Domain.Enum.StatusCode.OK)
            return responseByArticles.Data;

        return responseByArticles.Data;
    }
    
    [HttpPost]
    public async Task<List<ProfileEntity>> GetResultByProfiles(string searchLine)
    {
        var responseByProfiles = await GetResultAfterSearchByProfiles(searchLine);
        
        if (responseByProfiles.StatusCode == Domain.Enum.StatusCode.OK)
            return responseByProfiles.Data;

        return responseByProfiles.Data;
    }
    
    //Results
    public async Task<IBaseResponse<List<ProfileEntity>>> GetResultAfterSearchByProfiles(string searchLine)
    {
        try
        {
            
            var resultAfterSearchByProfiles = await profileRepository.GetAllAsync();
            
            var resultByProfiles = resultAfterSearchByProfiles
                .Where(x =>  x.Name == searchLine
                             || x.Description == searchLine)
                .ToList();
            
            var search = new SearchEntity()
            {
                Description = searchLine,
                Author = User.Identity.Name,
                SearchData = SearchData.Peoples
            };
            
            await searchRepository.CreateAsync(search);
            
            return new BaseResponse<List<ProfileEntity>>()
            {
                Data = resultByProfiles,
                Description = "Search created",
                StatusCode = Domain.Enum.StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            return new BaseResponse<List<ProfileEntity>>()
            {
                Description = exception.Message,
                StatusCode = Domain.Enum.StatusCode.InternalServerError
            };
        }
    }
    
    public async Task<IBaseResponse<List<ArticleEntity>>> GetResultAfterSearchByArticles(string searchLine)
    {
        try
        {
            var searchesByArticles = new List<ArticleEntity>();
            var resultAfterSearchByArticles = await articleRepository.GetAllAsync();
            
            var resultByArticles = resultAfterSearchByArticles
                .Where(x => x.Title == searchLine 
                            || x.Description == searchLine)
                .ToList();
            
            var search = new SearchEntity()
            {
                Description = searchLine,
                Author = User.Identity.Name,
                SearchData = SearchData.Articles
            };
            
            await searchRepository.CreateAsync(search);
            
            return new BaseResponse<List<ArticleEntity>>()
            {
                Data = resultByArticles,
                Description = "Search created",
                StatusCode = Domain.Enum.StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            return new BaseResponse<List<ArticleEntity>>()
            {
                Description = exception.Message,
                StatusCode = Domain.Enum.StatusCode.InternalServerError
            };
        }
    }
}