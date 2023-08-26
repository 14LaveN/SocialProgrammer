using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Enum;
using SocialProgrammer.Domain.Helpers;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Account;
using SocialProgrammer.Domain.ViewModels.MongoSettings;
using SocialProgrammer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SocialProgrammer.Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository<UserEntity> userRepository;
    private readonly ILogger<UserService> logger;

    public UserService(IUserRepository<UserEntity> userRepository,
        ILogger<UserService> logger)
    {
        this.userRepository = userRepository;
        this.logger = logger;
    }

    public async Task<IBaseResponse<ClaimsIdentity>> RegisterUser(RegisterViewModel registerViewModel)
    {
        try
        {
            logger.LogInformation($"Request for register a user - {registerViewModel.Name}");
            var user = await userRepository.GetNameAsync(registerViewModel.Name);
            if (user != null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "User with the same name already taken",
                };
            }

            user = new UserEntity()
            {
                Name = registerViewModel.Name,
                Role = Role.User,
                Password = HashPasswordHelper.HashPassowrd(registerViewModel.Password),
                Email = registerViewModel.Email,
                IsProfileCreated = false,
                CreationDate = DateTime.Now
            };

            await userRepository.CreateAsync(user);

            var result = Authenticate(user);

            logger.LogInformation($"User created - {registerViewModel.Name}");

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "User created",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[UserService.RegisterUser]: {exception.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<ClaimsIdentity>> LoginUser(LoginViewModel loginViewModel)
    {
        try
        {
            logger.LogInformation($"Request for login a user - {loginViewModel.Name}");
            var user = await userRepository.GetNameAsync(loginViewModel.Name);
            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "User with the same name not found",
                };
            }

            if (user.Password != HashPasswordHelper.HashPassowrd(loginViewModel.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Incorrect password"
                };
            }

            var result = Authenticate(user);

            logger.LogInformation($"User logined - {loginViewModel.Name}");

            return new BaseResponse<ClaimsIdentity>()
            {
                Data = result,
                Description = "User logined",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[UserService.LoginUser]: {exception.Message}");
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    private ClaimsIdentity Authenticate(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}
