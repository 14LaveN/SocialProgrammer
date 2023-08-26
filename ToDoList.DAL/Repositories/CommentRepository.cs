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

public class CommentRepository : ICommentRepository<CommentEntity>
{
    private readonly IMongoCollection<CommentEntity> commentsCollection;

    public CommentRepository(
        IOptions<Settings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        commentsCollection = mongoDatabase.GetCollection<CommentEntity>(
            dbSettings.Value.CommentsCollectionName);
    }

    public async Task<List<CommentEntity>> GetAllAsync() =>
        await commentsCollection.Find(_ => true).ToListAsync();

    public async Task<CommentEntity?> GetAsync(string id) =>
        await commentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<CommentEntity>> GetAllCommentsByArticleId(string articleId) =>
        await commentsCollection.Find(x => x.ArticleId == articleId).ToListAsync();

    public async Task CreateAsync(CommentEntity comment) =>
        await commentsCollection.InsertOneAsync(comment);

    public async Task UpdateAsync(string id, CommentEntity updatedComment) =>
        await commentsCollection.ReplaceOneAsync(x => x.Id == id, updatedComment);

    public async Task RemoveAsync(string id) =>
        await commentsCollection.DeleteOneAsync(x => x.Id == id);
}
