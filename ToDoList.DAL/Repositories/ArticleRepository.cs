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

public class ArticleRepository : IArticleRepository<ArticleEntity>
{
    private readonly IMongoCollection<ArticleEntity> articlesCollection;

    public ArticleRepository(
        IOptions<Settings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        articlesCollection = mongoDatabase.GetCollection<ArticleEntity>(
            dbSettings.Value.ArticlesCollectionName);
    }

    public async Task<List<ArticleEntity>> GetAllAsync() =>
        await articlesCollection.Find(_ => true).ToListAsync();

    public async Task<ArticleEntity?> GetAsync(string id) =>
        await articlesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<ArticleEntity?> GetNameAsync(string name) =>
        await articlesCollection.Find(x => x.Title == name).FirstOrDefaultAsync();

    public async Task CreateAsync(ArticleEntity newArticle) =>
        await articlesCollection.InsertOneAsync(newArticle);

    public async Task UpdateAsync(string id, ArticleEntity updatedArticle) =>
        await articlesCollection.ReplaceOneAsync(x => x.Id == id, updatedArticle);

    public async Task RemoveAsync(string id) =>
        await articlesCollection.DeleteOneAsync(x => x.Id == id);
}
