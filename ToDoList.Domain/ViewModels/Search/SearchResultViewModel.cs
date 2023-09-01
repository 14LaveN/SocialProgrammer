using SocialProgrammer.Domain.Enum;

namespace SocialProgrammer.Domain.ViewModels.Search;

public class SearchResultViewModel
{
    public required string Description { get; set; }
    public required SearchData SearchData { get; set; }
}