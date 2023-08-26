using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.Entity;

public class CommentEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? ArticleId { get; set; }

    public string Author { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; }
}