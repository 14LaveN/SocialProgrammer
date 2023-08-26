using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.ViewModels.Profile;

public class CreateProfileViewModel
{
    public required string Name { get; set; }

    public required string Description { get; set; }
}
