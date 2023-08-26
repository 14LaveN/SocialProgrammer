using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Interfaces;

public interface IArticleService
{
    Task<IBaseResponse<ArticleEntity>> CreateArticle(CreateArticleViewModel createArticleViewModel);

    Task<IBaseResponse<List<ArticleEntity>>> GetAll();

    Task<IBaseResponse<ArticleEntity>> DeleteArticle(string id);
}
