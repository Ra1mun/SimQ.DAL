using MongoDB.Bson.Serialization.Attributes;
using SimQCore.Modeller.BaseModels;

namespace SimQ.DAL.Models.ProblemAggregation;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(ServiceBlock))]
public abstract class Agent
{
    [BsonElement]
    public required string Id { get; set; }

    [BsonElement]
    public AgentType Type { get; protected set; }
    
    [BsonElement]
    public required string ReflectionType { get; set; }
}

[BsonDiscriminator("ServiceBlock")]
public class ServiceBlock : Agent
{
    public ServiceBlock()
    {
        Type = AgentType.ServiceBlock;
    }
    
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

[BsonDiscriminator("Source")]
public class Source : Agent
{
    public Source()
    {
        Type = AgentType.Source;
    }
}

[BsonDiscriminator("Buffer")]
public class Buffer : Agent
{
    public Buffer()
    {
        Type = AgentType.Buffer;
    }
}

