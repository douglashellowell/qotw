using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Questions.Models;

public class QuestionDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("question")] 
    public string Question { get; set; }
    
    [BsonElement("startDate")]
    public DateTime StartDate { get; set; }
    
    [BsonElement("endDate")]
    public DateTime EndDate { get; set; }
    
    [BsonElement("createdDate")]
    public DateTime? CreatedDate { get; set; }
}