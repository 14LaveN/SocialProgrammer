using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialProgrammer.Domain.Enum;

namespace SocialProgrammer.Domain.Entity;

public class SearchEntity
{
    public string? Id { get; set; }

    public string Author { get; set; }

    public string Description { get; set; }

    public SearchData SearchData { get; set; }
}