using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SocialProgrammer.Domain.Enum;

namespace SocialProgrammer.Domain.Entity;

public class SearchEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Author { get; set; }
    

    public string Description { get; set; }
    

    public SearchData SearchData { get; set; }
}