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

public class LikeRepository : ILikeRepository<LikeEntity>
{
    private readonly IMongoCollection<LikeEntity> likesCollection;

    public LikeRepository(
        IOptions<Settings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        likesCollection = mongoDatabase.GetCollection<LikeEntity>(
            dbSettings.Value.LikesCollectionName);
    }

    public async Task<List<LikeEntity>> GetAllAsync() =>
        await likesCollection.Find(_ => true).ToListAsync();

    public async Task<LikeEntity?> GetAsync(string id) =>
        await likesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<LikeEntity?> GetUserNameAsync(string userName) =>
        await likesCollection.Find(x => x.UserName == userName).FirstOrDefaultAsync();

    public async Task CreateAsync(LikeEntity like) =>
        await likesCollection.InsertOneAsync(like);

    public async Task RemoveAsync(string id) =>
        await likesCollection.DeleteOneAsync(x => x.Id == id);
}
