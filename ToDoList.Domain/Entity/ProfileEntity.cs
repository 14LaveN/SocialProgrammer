using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Domain.Entity;

public class ProfileEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; }  = string.Empty;

    public string Description { get; set; }  = string.Empty;

    public int ArticlesCount { get; set; } = 0;

    public DateTime CreationDate { get; set; } = DateTime.Now;
}
