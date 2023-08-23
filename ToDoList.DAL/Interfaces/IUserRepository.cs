using MongoDB.Driver;
using SocialProgrammer.Domain.Entity;

namespace SocialProgrammer.DAL.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> GetAllUsers();

    Task<UserEntity> GetUser(string id);

    Task AddUser(UserEntity item);

    Task<DeleteResult> RemoveUser(string id);

    //Task<UpdateResult> UpdateUser(string id, string body);
}