using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocialProgrammer.Domain.Entity;

public class MessageEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? RecipientName { get; set; }

    public string Author { get; set; }

    public string Message { get; set; }

    public DateTime CreationDate { get; set; }
}