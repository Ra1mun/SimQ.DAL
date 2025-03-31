using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Simq.Dal.Models;

public class Problem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public List<IAgentModel> Agents { get; set; }
}