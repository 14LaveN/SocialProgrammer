using System.ComponentModel.DataAnnotations;
using SocialProgrammer.Domain.Enum;

namespace SocialProgrammer.Domain.ViewModels.Search;

public class SearchResultViewModel
{
    [MinLength(0, ErrorMessage = "Your description too small")]
    [MaxLength(250, ErrorMessage = "Your description too big")]
    public required string Description { get; set; }
}