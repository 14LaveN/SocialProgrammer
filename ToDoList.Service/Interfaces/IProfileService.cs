using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Interfaces;

public interface IProfileService
{
    Task<IBaseResponse<ProfileEntity>> CreateProfile(CreateProfileViewModel createProfileViewModel);

    Task<IBaseResponse<ProfileEntity>> DeleteProfile(string id);
}
