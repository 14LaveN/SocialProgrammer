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

public class UserRepository : IUserRepository<UserEntity>
{
    private readonly IMongoCollection<UserEntity> usersCollection;

    public UserRepository(
        IOptions<Settings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        usersCollection = mongoDatabase.GetCollection<UserEntity>(
            dbSettings.Value.UsersCollectionName);
    }

    public async Task<List<UserEntity>> GetAllAsync() =>
        await usersCollection.Find(_ => true).ToListAsync();

    public async Task<UserEntity?> GetAsync(string id) =>
        await usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<UserEntity?> GetNameAsync(string name) =>
        await usersCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

    public async Task CreateAsync(UserEntity newUser) =>
        await usersCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, UserEntity updatedBook) =>
        await usersCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await usersCollection.DeleteOneAsync(x => x.Id == id);
}
