using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.DAL.Repositories;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Enum;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Article;
using SocialProgrammer.Domain.ViewModels.Profile;
using SocialProgrammer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Implementations;

public class ArticleService : IArticleService
{
    private readonly ILogger<ArticleService> logger;
    private readonly IArticleRepository<ArticleEntity> articleRepository;

    public ArticleService(ILogger<ArticleService> logger,
        IArticleRepository<ArticleEntity> articleRepository)
    {
        this.logger = logger;
        this.articleRepository = articleRepository; 
    }

    public async Task<IBaseResponse<ArticleEntity>> CreateArticle(CreateArticleViewModel createArticleViewModel)
    {
        try
        {
            logger.LogInformation($"Request for create an article - {createArticleViewModel.Title}");

            var article = await articleRepository.GetNameAsync(createArticleViewModel.Title);

            if (article != null)
            {
                return new BaseResponse<ArticleEntity>()
                {
                    Description = "Article with the same name already taken",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            article = new ArticleEntity()
            {
                Title = createArticleViewModel.Title,
                Description = createArticleViewModel.Description,
                CreationDate = DateTime.Now,
                LikesCount = 0,
                ContentType = createArticleViewModel.ContentType,
                CommentsCount = 0,
                Author = createArticleViewModel.Author
            };

            await articleRepository.CreateAsync(article);

            logger.LogInformation($"Article created: {article.Title}");
            return new BaseResponse<ArticleEntity>()
            {
                Description = "Article created",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[ArticleService.CreateArticle]: {exception.Message}");
            return new BaseResponse<ArticleEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<ArticleEntity>> DeleteArticle(string id)
    {
        try
        {
            var profile = await articleRepository.GetAsync(id);

            logger.LogInformation($"Request for delete a article - {profile.Title}");

            if (profile == null)
            {
                return new BaseResponse<ArticleEntity>()
                {
                    Description = "Article with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            await articleRepository.RemoveAsync(id);

            logger.LogInformation($"Article deleted: {profile.Title}");

            return new BaseResponse<ArticleEntity>()
            {
                Description = "Article deleted",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[ArticleService.DeleteArticle]: {exception.Message}");
            return new BaseResponse<ArticleEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<List<ArticleEntity>>> GetAll()
    {
        try
        {
            var articles = await articleRepository.GetAllAsync();

            logger.LogInformation($"Request for create an articles");

            if (articles == null)
            {
                return new BaseResponse<List<ArticleEntity>>()
                {
                    Description = "Articles not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            logger.LogInformation($"Get articles");
            return new BaseResponse<List<ArticleEntity>>()
            {
                Data = articles,
                Description = "Get articles",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[ArticleService.GetAll]: {exception.Message}");
            return new BaseResponse<List<ArticleEntity>>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
