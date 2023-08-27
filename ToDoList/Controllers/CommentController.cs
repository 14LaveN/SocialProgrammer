using Microsoft.AspNetCore.Mvc;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.Article;
using SocialProgrammer.Domain.ViewModels.Comment;
using SocialProgrammer.Service.Implementations;
using SocialProgrammer.Service.Interfaces;

namespace SocialProgrammer.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService commentService;
    private readonly ICommentRepository<CommentEntity> commentRepository;

    public CommentController(ICommentService commentService,
        ICommentRepository<CommentEntity> commentRepository)
    {
        this.commentService = commentService;
        this.commentRepository = commentRepository;
    }

    public async Task<IActionResult> CommentForm(string id)
    {
        return View(await GetAll(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentViewModel createCommentViewModel)
    {
        var response = await commentService.CreateComment(createCommentViewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }
        return BadRequest(new { description = response.Description });
    }

    [HttpGet]
    public async Task<List<CommentEntity>?> GetAll(string articleId)
    {
        var response = await commentService.GetAll(articleId);
        return response.Data;
    }

    [HttpPost]
    public async Task<IActionResult> DeleteComment(string articleId, string commentId)
    {
        var comment = await commentRepository.GetAsync(commentId);
        if (comment.Author == User.Identity.Name)
        {
            var response = await commentService.DeleteComment(articleId, commentId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Redirect($"https://localhost:44333/Comment/CommentForm/{articleId}");
            }
        }
        return Redirect($"https://localhost:44333/Comment/CommentForm/{articleId}");
    }
}