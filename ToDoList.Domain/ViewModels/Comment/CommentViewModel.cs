using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.ViewModels.Comment;

public class CommentViewModel
{
    public required string Author { get; set; }

    [MinLength (1, ErrorMessage = "Your description too small")]
    [MaxLength (234, ErrorMessage = "Your description too big")]
    public required string Description { get; set; }

    public required DateTime CreationDate { get; set; }
}