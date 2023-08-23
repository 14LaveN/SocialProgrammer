using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.MongoSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Implementations;

public class UserService
{
    private readonly IMongoCollection<UserEntity> booksCollection;

    public UserService(
        IOptions<Settings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        booksCollection = mongoDatabase.GetCollection<UserEntity>(
            dbSettings.Value.UsersCollectionName);
    }

    public async Task<List<UserEntity>> GetAsync() =>
        await booksCollection.Find(_ => true).ToListAsync();

    public async Task<UserEntity?> GetAsync(string id) =>
        await booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(UserEntity newUser) =>
        await booksCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, UserEntity updatedBook) =>
        await booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await booksCollection.DeleteOneAsync(x => x.Id == id);
}
