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
    
    [BsonElement("submitted_by")]
    public string SubmittedBy { get; set; }
    
    [BsonElement("answer")] 
    public string Answer { get; set; }
    
    [BsonElement("createdDate")]
    public DateTime? CreatedDate { get; set; }
}