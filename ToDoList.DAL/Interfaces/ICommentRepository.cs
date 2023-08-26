using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.DAL.Interfaces;

public interface ICommentRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetAsync(string id);

    Task<List<T>> GetAllCommentsByArticleId(string articleId);

    Task CreateAsync(T comment);

    Task UpdateAsync(string id, T updatedComment);

    Task RemoveAsync(string id);
}
