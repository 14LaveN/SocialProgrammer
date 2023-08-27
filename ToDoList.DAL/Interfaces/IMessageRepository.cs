using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.DAL.Interfaces;

public interface IMessageRepository<T>
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetAsync(string id);

    Task<T?> GetNameAsync(string name);

    Task CreateAsync(T message);

    Task UpdateAsync(string id, T updatedMessage);

    Task RemoveAsync(string id);
}
