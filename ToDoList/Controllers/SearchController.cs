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
    private readonly ILogger<SearchController> logger;
    private readonly ISearchRepository<SearchEntity> searchRepository;
    private readonly IArticleRepository<ArticleEntity> articleRepository;
    private readonly IProfileRepository<ProfileEntity> profileRepository;
    
    public SearchController(ILogger<SearchController> logger,
        ISearchRepository<SearchEntity> searchRepository,
        IArticleRepository<ArticleEntity> articleRepository,
        IProfileRepository<ProfileEntity> profileRepository)
    {
        this.logger = logger;
        this.searchRepository = searchRepository;
        this.articleRepository = articleRepository;
        this.profileRepository = profileRepository;
    }
    [HttpGet]
    public async Task<IActionResult> SearchForm(string searchLine, SearchData searchData)
    {
        if (searchData == SearchData.Articles)
            return View(await GetResultByArticles(searchLine));
        
        return View(await GetResultByArticles(searchLine));
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
    
    public async Task<IBaseResponse<List<ProfileEntity>>> GetResultAfterSearchByProfiles(string searchLine)
    {
        try
        {
            logger.LogInformation($"Request for search - {searchLine}");
            
            var searchesByProfiles = new List<ProfileEntity>();
            var resultAfterSearchByProfiles = await profileRepository.GetAllAsync();
            
            var resultByProfiles = resultAfterSearchByProfiles
                .Where(x => x.Description == searchLine 
                            || x.Name == searchLine)
                .ToList();
            
            var search = new SearchEntity()
            {
                Description = searchLine,
                Author = User.Identity.Name,
                SearchData = SearchData.Peoples
            };
            
            await searchRepository.CreateAsync(search);

            logger.LogInformation($"Search created: {search.Description}");
            return new BaseResponse<List<ProfileEntity>>()
            {
                Data = resultByProfiles,
                Description = "Search created",
                StatusCode = Domain.Enum.StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[ProfileService.CreateProfile]: {exception.Message}");
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
            logger.LogInformation($"Request for search - {searchLine}");

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

            logger.LogInformation($"Search created: {search.Description}");
            return new BaseResponse<List<ArticleEntity>>()
            {
                Data = resultByArticles,
                Description = "Search created",
                StatusCode = Domain.Enum.StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[SearchController.GetResultByArticles]: {exception.Message}");
            return new BaseResponse<List<ArticleEntity>>()
            {
                Description = exception.Message,
                StatusCode = Domain.Enum.StatusCode.InternalServerError
            };
        }
    }
}