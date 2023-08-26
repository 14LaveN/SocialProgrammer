using MongoDB.Driver;
using SocialProgrammer.Domain.Entity;

namespace SocialProgrammer.DAL.Interfaces;

public interface IUserRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetAsync(string id);

    Task<T?> GetNameAsync(string name);

    Task CreateAsync(T newUser);

    Task UpdateAsync(string id, T updatedBook);

    Task RemoveAsync(string id);
}