using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.MongoSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext userContext = null;

    public UserRepository(IOptions<Settings> settings)
    {
        userContext = new UserContext(settings);
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsers()
    {
        return await userContext.Users.Find(_ => true).ToListAsync();
    }

    public async Task<UserEntity> GetUser(string id)
    {
        var filter = Builders<UserEntity>.Filter.Eq("Id", id);
        return await userContext.Users
                        .Find(filter)
                        .FirstOrDefaultAsync();
    }

    public async Task AddUser(UserEntity item)
    {
        await userContext.Users.InsertOneAsync(item);
    }

    public async Task<DeleteResult> RemoveUser(string id)
    {
        return await userContext.Users.DeleteOneAsync(
             Builders<UserEntity>.Filter.Eq("Id", id));
    }

    //public async Task<UpdateResult> UpdateNote(string id, string body)
    //{
    //    var filter = Builders<UserEntity>.Filter.Eq(x => x.id, id);
    //    var update = Builders<UserEntity>.Update
    //                    .Set(s => s.Body, body)
    //                    .CurrentDate(s => s.UpdatedOn);
    //
    //    return await userContext.Users.UpdateOneAsync(filter, update);
    //}
}
