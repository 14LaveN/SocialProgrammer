using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Enum;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Profile;
using SocialProgrammer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Implementations;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository<ProfileEntity> profileRepository;
    private readonly ILogger<ProfileService> logger;

    public ProfileService(ILogger<ProfileService> logger,
        IProfileRepository<ProfileEntity> profileRepository)
    {
        this.logger = logger;
        this.profileRepository = profileRepository;
    }
    public async Task<IBaseResponse<ProfileEntity>> CreateProfile(CreateProfileViewModel createProfileViewModel)
    {
        try
        {
            logger.LogInformation($"Request for create a profile - {createProfileViewModel.Name}");

            var profile = await profileRepository.GetNameAsync(createProfileViewModel.Name);
            if (profile != null)
            {
                return new BaseResponse<ProfileEntity>()
                {
                    Description = "Profile with the same name already taken",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            profile = new ProfileEntity()
            {
                Name = createProfileViewModel.Name,
                Description = createProfileViewModel.Description,
                CreationDate = DateTime.Now,
                ArticlesCount = 0
            };
            await profileRepository.CreateAsync(profile);

            logger.LogInformation($"Profile created: {profile.Name}");
            return new BaseResponse<ProfileEntity>()
            {
                Description = "Profile created",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[ProfileService.CreateProfile]: {exception.Message}");
            return new BaseResponse<ProfileEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
