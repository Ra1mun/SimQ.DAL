using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimQ.DAL.Models.ProblemAggregation;

public class Problem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public List<Agent> Agents { get; set; }
}