using Microsoft.AspNetCore.Mvc;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.DAL.Repositories;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.Profile;
using SocialProgrammer.Service.Interfaces;

namespace SocialProgrammer.Controllers;

public class ProfileController : Controller
{
    private readonly IProfileService profileService;
    private readonly IUserRepository<UserEntity> userRepository;
    private readonly IProfileRepository<ProfileEntity> profileRepository;

    public ProfileController(IProfileService profileService,
        IUserRepository<UserEntity> userRepository,
        IProfileRepository<ProfileEntity> profileRepository)
    {
        this.profileService = profileService;
        this.userRepository = userRepository;   
        this.profileRepository = profileRepository;
    }

    [HttpGet]
    public IActionResult CreateProfileForm() => View();

    [HttpGet]
    public async Task<IActionResult> ProfileForm()
    {
        var profile = await profileRepository.GetNameAsync(User.Identity.Name);

        if (profile is null)
        {
            return RedirectToAction("CreateProfileForm", "Profile");
        }
        
        return View(profile);
    }

    [HttpPost]
    public async Task<IActionResult> SearchedProfileForm(string id)
    {
        var profile = await profileRepository.GetAsync(id);
        return View(profile);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile(CreateProfileViewModel createProfileViewModel)
    {
        var response = await profileService.CreateProfile(createProfileViewModel);

        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            var user = await userRepository.GetNameAsync(User.Identity.Name);
            user.IsProfileCreated = true;
            await userRepository.UpdateAsync(user.Id, user);

            return Ok(new { description = response.Description });
        }
        return BadRequest(new { description = response.Description });
    }
}
