using Amazon.Runtime.Internal;
using Microsoft.Extensions.Logging;
using NLog;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Enum;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Search;
using SocialProgrammer.Service.Interfaces;

namespace SocialProgrammer.Service.Implementations;

public class SearchService : ISearchService
{
    private readonly ILogger<SearchService> logger;
    private readonly ISearchRepository<SearchEntity> searchRepository;

    public SearchService(ISearchRepository<SearchEntity> searchRepository,
        ILogger<SearchService> logger)
    {
        this.logger = logger;
        this.searchRepository = this.searchRepository;
    }
    
    public async Task<IBaseResponse<List<SearchResultViewModel>>> GetResult(string searchLine)
    {
        try
        {
            logger.LogInformation($"Request for search - {searchLine}");

            var search = new SearchEntity();
            
            search = new SearchEntity()
            {
                Description = searchLine,
                Author = HttpContent.User.Identity.Name,
                SearchData = SearchData.Articles
            };
            await searchRepository.CreateAsync(search);

            logger.LogInformation($"Search created: {search.Description}");
            return new BaseResponse<List<SearchResultViewModel>()
            {
                Description = "Search created",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[ProfileService.CreateProfile]: {exception.Message}");
            return new BaseResponse<ProfileEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}