using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SocialProgrammer.Domain.Enum;

namespace SocialProgrammer.Domain.Entity;

public class ArticleEntity
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } 

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public ContentTypes ContentType { get; set; } = ContentTypes.Java;

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public int LikesCount { get; set; } = 0;

    public int CommentsCount { get; set; } = 0;
}