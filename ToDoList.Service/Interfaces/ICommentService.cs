using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Interfaces;

public interface ICommentService
{
    Task<IBaseResponse<CommentEntity>> CreateComment(CreateCommentViewModel createCommentViewModel);

    Task<IBaseResponse<CommentEntity>> DeleteComment(string articleId, string commentId);

    Task<IBaseResponse<CommentEntity>> UpdateComment(string articleId, string commentId, CreateCommentViewModel createCommentViewModel);

    Task<IBaseResponse<List<CommentEntity>>> GetAll(string articleId);
}
