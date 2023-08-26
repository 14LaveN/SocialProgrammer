using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SocialProgrammer.Domain.Entity;

public class LikeEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? ArticleId { get; set; }

    public string UserName { get; set; }
}
