using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.Entity;

public class ProfileEntity
{
    public string? id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public int ArticlesCount { get; set; }

    public IFormFile Photo { get; set; }

    public DateOnly CreationDate { get; set; }
}
