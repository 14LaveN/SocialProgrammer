using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.DAL.Repositories;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Enum;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Article;
using SocialProgrammer.Domain.ViewModels.Comment;
using SocialProgrammer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Implementations;

public class CommentService : ICommentService
{
    private readonly ILogger<CommentService> logger;
    private readonly ICommentRepository<CommentEntity> commentRepository;
    private readonly IArticleRepository<ArticleEntity> articleRepository;

    public CommentService(ILogger<CommentService> logger,
        ICommentRepository<CommentEntity> commentRepository,
        IArticleRepository<ArticleEntity> articleRepository)
    {
        this.logger = logger;   
        this.commentRepository = commentRepository; 
        this.articleRepository = articleRepository;
    }

    public async Task<IBaseResponse<CommentEntity>> CreateComment(CreateCommentViewModel createCommentViewModel)
    {
        try
        {
            var article = await articleRepository.GetAsync(createCommentViewModel.ArticleId);
            logger.LogInformation($"Request for create a comment to article - {article.Title}");

            if (article == null)
            {
                return new BaseResponse<CommentEntity>()
                {
                    Description = "Article with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            var comment = new CommentEntity()
            {
                Description = createCommentViewModel.Description,
                CreationDate = DateTime.Now,
                ArticleId = createCommentViewModel.ArticleId,
                Author = createCommentViewModel.Author
            };

            await commentRepository.CreateAsync(comment);
            article.CommentsCount++;
            await articleRepository.UpdateAsync(createCommentViewModel.ArticleId, article);

            logger.LogInformation($"Comment created: {comment.Author} {comment.CreationDate}");
            return new BaseResponse<CommentEntity>()
            {
                Description = "Comment created",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[CommentService.CreateComment]: {exception.Message}");
            return new BaseResponse<CommentEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<CommentEntity>> DeleteComment(string articleId, string commentId)
    {
        try
        {
            var article = await articleRepository.GetAsync(articleId);
            var comment = await commentRepository.GetAsync(commentId);

            logger.LogInformation($"Request for delete a comment from article - {article.Title}");

            if (article == null)
            {
                return new BaseResponse<CommentEntity>()
                {
                    Description = "Article with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            await commentRepository.RemoveAsync(commentId);
            article.CommentsCount--;
            await articleRepository.UpdateAsync(articleId, article);

            logger.LogInformation($"Comment deleted: {comment.Author} {comment.CreationDate}");
            return new BaseResponse<CommentEntity>()
            {
                Description = "Comment created",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[CommentService.DeleteComment]: {exception.Message}");
            return new BaseResponse<CommentEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<List<CommentEntity>>> GetAll(string articleId)
    {
        try
        {
            logger.LogInformation("Request for get comments from article");

            var comments = await commentRepository.GetAllCommentsByArticleId(articleId);

            logger.LogInformation("Get comments");
          
            return new BaseResponse<List<CommentEntity>>()
            {
                Data = comments,
                Description = "Comment created",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[CommentService.GetAll]: {exception.Message}");
            return new BaseResponse<List<CommentEntity>>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<CommentEntity>> UpdateComment(string articleId, string commentId, CreateCommentViewModel createCommentViewModel)
    {
        try
        {
            var article = await articleRepository.GetAsync(articleId);
            var comment = await commentRepository.GetAsync(commentId);

            logger.LogInformation($"Request for delete a comment from article - {article.Title}");

            if (article == null)
            {
                return new BaseResponse<CommentEntity>()
                {
                    Description = "Article with the same name not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            var newComment = new CommentEntity
            {
                Description = createCommentViewModel.Description,
                CreationDate = DateTime.Now,
                ArticleId = articleId,
                Author = createCommentViewModel.Author
            };

            await commentRepository.UpdateAsync(commentId, newComment);

            logger.LogInformation($"Comment updated: {comment.Author} {comment.CreationDate}");
            return new BaseResponse<CommentEntity>()
            {
                Description = "Comment updated",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[CommentService.DeleteComment]: {exception.Message}");
            return new BaseResponse<CommentEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
