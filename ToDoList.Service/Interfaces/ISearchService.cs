using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Search;

namespace SocialProgrammer.Service.Interfaces;

public interface ISearchService
{
    Task<IBaseResponse<List<SearchResultViewModel>>> GetResult(string searchLine);
}