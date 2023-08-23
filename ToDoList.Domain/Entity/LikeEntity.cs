namespace SocialProgrammer.Domain.Entity;

public class LikeEntity
{
    public string? Id { get; set; }

    public long ArticleId { get; set; }

    public string UserName { get; set; }
}
