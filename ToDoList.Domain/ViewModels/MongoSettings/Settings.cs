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
}
