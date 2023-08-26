using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.ViewModels.Profile;

public class ProfileViewModel
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Email { get; set; }

    public required int ArticlesCount { get; set; }

    public required DateTime CreationDate { get; set; }
}
