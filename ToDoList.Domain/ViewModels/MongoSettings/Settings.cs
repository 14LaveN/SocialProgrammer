using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.ViewModels.MongoSettings;

public class Settings
{
    public string ConnectionString { get; set; } = null!;

    public string Database { get; set; } = null!;

    public string UsersCollectionName { get; set; } = null!;

    public string ProfilesCollectionName { get; set;} = null!;

    public string ArticlesCollectionName { get; set;} = null!;

    public string LikesCollectionName { get; set; } = null!;

    public string CommentsCollectionName { get; set; } = null!;

    public string MessagesCollectionName { get; set; } = null!;

    public string MessagesHistoryCollectionName { get; set; } = null!;
    
    public string SearchesCollectionName { get; set; } = null!;
}
