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

public class ProfileRepository : IProfileRepository<ProfileEntity>
{
    private readonly IMongoCollection<ProfileEntity> profilesCollection;

    public ProfileRepository(
        IOptions<Settings> dbSettings)
    {
        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.Database);

        profilesCollection = mongoDatabase.GetCollection<ProfileEntity>(
            dbSettings.Value.ProfilesCollectionName);
    }

    public async Task<List<ProfileEntity>> GetAllAsync() =>
        await profilesCollection.Find(_ => true).ToListAsync();

    public async Task<ProfileEntity?> GetAsync(string id) =>
        await profilesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<ProfileEntity?> GetNameAsync(string name) =>
        await profilesCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

    public async Task CreateAsync(ProfileEntity newProfile) =>
        await profilesCollection.InsertOneAsync(newProfile);

    public async Task UpdateAsync(string id, ProfileEntity updatedProfile) =>
        await profilesCollection.ReplaceOneAsync(x => x.Id == id, updatedProfile);

    public async Task RemoveAsync(string id) =>
        await profilesCollection.DeleteOneAsync(x => x.Id == id);
}
