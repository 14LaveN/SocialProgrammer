using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.DAL.Interfaces;

public interface ILikeRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetAsync(string id);

    Task<T?> GetUserNameAsync(string userName);

    Task CreateAsync(T newLike);

    Task RemoveAsync(string id);
}