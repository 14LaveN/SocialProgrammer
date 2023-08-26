using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using SocialProgrammer.Domain.Enum;

namespace SocialProgrammer.Domain.Entity;

public class UserEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }

    public bool IsProfileCreated { get; set; }

    public DateTime CreationDate { get; set; }
}