using Microsoft.AspNetCore.Mvc;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.DAL.Repositories;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Response;
using Microsoft.IdentityModel.Tokens;

namespace SocialProgrammer.Controllers;

public class LikeController : Controller
{
    private readonly ILikeRepository<LikeEntity> likeRepository;
    private readonly IArticleRepository<ArticleEntity> articleRepository;

    public LikeController(ILikeRepository<LikeEntity> likeRepository,
        IArticleRepository<ArticleEntity> articleRepository)
    {
        this.likeRepository = likeRepository;
        this.articleRepository = articleRepository;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddLike(string id)
    {
        var response = await AddLikeToPost(id);
        return RedirectToAction("ArticleForm", "Article"); 
    }

    public async Task<IBaseResponse<LikeEntity>> AddLikeToPost(string id)
    {
        try
        {
            var likes = await likeRepository.GetAllAsync();
            var article = await articleRepository.GetAsync(id);
            var likesByArticleId = likes.Where(x => x.ArticleId == id)
                .Where(x => x.UserName == User.Identity.Name);

            if (article == null)
            {
                return new BaseResponse<LikeEntity>()
                {
                    Description = "Article this the same id not found",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }

            if (likesByArticleId.IsNullOrEmpty())
            {
                var like = new LikeEntity
                {
                    ArticleId = id,
                    UserName = User.Identity.Name
                };

                await likeRepository.CreateAsync(like);

                article.LikesCount++;
                await articleRepository.UpdateAsync(id, article);
            }

            return new BaseResponse<LikeEntity>()
            {
                Description = "Add like",
                StatusCode = Domain.Enum.StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            return new BaseResponse<LikeEntity>()
            {
                Description = exception.Message,
                StatusCode = Domain.Enum.StatusCode.InternalServerError
            };
        }
    }
}
