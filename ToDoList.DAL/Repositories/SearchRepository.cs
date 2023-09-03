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

public class SearchRepository : ISearchRepository<SearchEntity>
{
    private readonly IMongoCollection<SearchEntity> searchesCollection;

    public SearchRepository(
        IOptions<Settings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        searchesCollection = mongoDatabase.GetCollection<SearchEntity>(
            dbSettings.Value.SearchesCollectionName);
    }

    public async Task<List<SearchEntity>> GetAllAsync() =>
        await searchesCollection.Find(_ => true).ToListAsync();

    public async Task<SearchEntity?> GetAsync(string id) =>
        await searchesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<SearchEntity?> GetAuthorAsync(string author) =>
        await searchesCollection.Find(x => x.Author == author).FirstOrDefaultAsync();

    public async Task CreateAsync(SearchEntity newSearch) =>
        await searchesCollection.InsertOneAsync(newSearch);

    public async Task RemoveAsync(string id) =>
        await searchesCollection.DeleteOneAsync(x => x.Id == id);
}