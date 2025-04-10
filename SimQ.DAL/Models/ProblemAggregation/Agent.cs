using MongoDB.Bson.Serialization.Attributes;
using SimQCore.Modeller.BaseModels;

namespace SimQ.DAL.Models.ProblemAggregation;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(ServiceBlock))]
public class Agent
{
    [BsonElement]
    public AgentType Type { get; set; }
    
    [BsonElement]
    public required string ReflectionType { get; set; }
    
    [BsonElement]
    public required string Id { get; set; }

    //List<string> LinkedAgents { get; set; }
}

[BsonDiscriminator("ServiceBlock")]
public class ServiceBlock : Agent
{
    [BsonElement]
    public List<Agent> BindedBuffer { get; set; } = new();

    public void BindBuffer(Agent agent)
    {
        if (agent.Type != AgentType.Buffer)
        {
            return;
        }
        
        BindedBuffer.Add(agent);
    }
}

