using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.MongoSettings;

namespace SocialProgrammer.DAL;

public class UserContext
{
    private readonly IMongoDatabase database = null;

    public UserContext(IOptions<Settings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        if (client != null)
            database = client.GetDatabase(settings.Value.Database);
    }
    public IMongoCollection<UserEntity> Users
    {
        get => database.GetCollection<UserEntity>("Users");
    }
}