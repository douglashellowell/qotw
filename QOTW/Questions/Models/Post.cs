using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Questions.Models;

public class Post
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("Title")] public string PostTitle { get; set; }

    [BsonElement("Author")] public string PostAuthor { get; set; }

}