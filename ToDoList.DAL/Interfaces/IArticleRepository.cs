using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.DAL.Interfaces;

public interface IArticleRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetAsync(string id);

    Task<T?> GetNameAsync(string name);

    Task CreateAsync(T newProfile);

    Task UpdateAsync(string id, T updatedProfile);

    Task RemoveAsync(string id);
}
