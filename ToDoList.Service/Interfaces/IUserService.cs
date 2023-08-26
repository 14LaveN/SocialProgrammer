using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Interfaces;

public interface IUserService
{
    Task<IBaseResponse<ClaimsIdentity>> RegisterUser(RegisterViewModel registerViewModel);

    Task<IBaseResponse<ClaimsIdentity>> LoginUser(LoginViewModel loginViewModel);
}