using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.Entity;

public class MessageEntity
{
    public string? Id { get; set; }

    public string Author { get; set; }

    public string Message { get; set; }

    public DateTime CreationDate { get; set; }
}