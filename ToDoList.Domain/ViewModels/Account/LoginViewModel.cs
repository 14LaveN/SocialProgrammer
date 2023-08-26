using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.ViewModels.Account;

public class LoginViewModel
{
    [Range (2,24, ErrorMessage = "Your name doesn't fit")]
    public required string Name { get; set; }

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Range(6, 24, ErrorMessage = "Your password doesn't fit")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
