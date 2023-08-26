using SocialProgrammer.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.ViewModels.Article;

public class ArticleViewModel
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string Author { get; set; }

    public required ContentTypes ContentType { get; set; }

    public required DateTime CreationDate { get; set; }

    public required int LikesCount { get; set; }

    public required int CommentsCount { get; set; }
}