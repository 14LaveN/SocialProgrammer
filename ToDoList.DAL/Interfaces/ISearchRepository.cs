using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.DAL.Interfaces;

public interface ISearchRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetAsync(string id);

    Task<T?> GetAuthorAsync(string author);

    Task CreateAsync(T newProfile);

    Task RemoveAsync(string id);
}