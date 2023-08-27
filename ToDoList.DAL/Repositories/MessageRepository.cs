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

public class MessageRepository : IMessageRepository<MessageEntity>
{
    private readonly IMongoCollection<MessageEntity> messagesCollection;

    public MessageRepository(
        IOptions<Settings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        messagesCollection = mongoDatabase.GetCollection<MessageEntity>(
            dbSettings.Value.MessagesCollectionName);
    }

    public async Task<List<MessageEntity>> GetAllAsync() =>
        await messagesCollection.Find(_ => true).ToListAsync();

    public async Task<MessageEntity?> GetAsync(string id) =>
        await messagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<MessageEntity?> GetNameAsync(string name) =>
        await messagesCollection.Find(x => x.Author == name).FirstOrDefaultAsync();

    public async Task CreateAsync(MessageEntity newProfile) =>
        await messagesCollection.InsertOneAsync(newProfile);

    public async Task UpdateAsync(string id, MessageEntity updatedProfile) =>
        await messagesCollection.ReplaceOneAsync(x => x.Id == id, updatedProfile);

    public async Task RemoveAsync(string id) =>
        await messagesCollection.DeleteOneAsync(x => x.Id == id);
}
