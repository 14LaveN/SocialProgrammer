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

public class MessageHistoryRepository : IMessageHistoryRepository<MessageHistoryEntity>
{
    private readonly IMongoCollection<MessageHistoryEntity> messagesCollection;

    public MessageHistoryRepository(
        IOptions<Settings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        messagesCollection = mongoDatabase.GetCollection<MessageHistoryEntity>(
            dbSettings.Value.MessagesHistoryCollectionName);
    }

    public async Task<List<MessageHistoryEntity>> GetAllAsync() =>
        await messagesCollection.Find(_ => true).ToListAsync();

    public async Task<MessageHistoryEntity?> GetAsync(string id) =>
        await messagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<MessageHistoryEntity?> GetNameAsync(string name) =>
        await messagesCollection.Find(x => x.Author == name).FirstOrDefaultAsync();

    public async Task CreateAsync(MessageHistoryEntity newMessage) =>
        await messagesCollection.InsertOneAsync(newMessage);

    public async Task UpdateAsync(string id, MessageHistoryEntity updatedMessage) =>
        await messagesCollection.ReplaceOneAsync(x => x.Id == id, updatedMessage);

    public async Task RemoveAsync(string id) =>
        await messagesCollection.DeleteOneAsync(x => x.Id == id);
}
