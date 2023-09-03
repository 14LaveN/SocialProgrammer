using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocialProgrammer.Domain.ViewModels.Message;

public class MessageViewModel
{
    public required string RecipientId { get; set; }

    public required string Author { get; set; }

    [MinLength(1, ErrorMessage = "Your message too small")]
    [MaxLength (234, ErrorMessage = "Your message too big")]
    public required string Message { get; set; }
}