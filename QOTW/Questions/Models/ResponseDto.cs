using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Questions.Models;

public class ResponseDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("questionId")]
    public string QuestionId { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("response")] 
    public string response { get; set; }
    
    [BsonElement("createdDate")]
    public DateTime? CreatedDate { get; set; }
}