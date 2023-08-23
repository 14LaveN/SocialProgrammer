using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.Entity;

public class CommentEntity
{
    public string? Id { get; set; }

    public long ArticleId { get; set; }

    public string Author { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }
}
