namespace Simq.Dal.Models;

public interface IAgentModel
{
    string Name { get; set; }

    AgentType Type { get; set; }

    //List<string> LinkedAgents { get; set; }
}

public enum AgentType
{
    Source,
    ServiceBlock,
    Buffer,
    Orbit
}

public class Source(int id) : IAgentModel
{
    public string Name { get; set; } = "SRC_" + id;

    public AgentType Type { get; set; } = AgentType.Source;
    
    public IDistribution Distribution { get; set; }
}

public class ServiceBlock(int id) : IAgentModel
{
    public string Name { get; set; } = "SBLOCK_" + id;

    public AgentType Type { get; set; } = AgentType.ServiceBlock;
    
    public IDistribution Distribution { get; set; }
}

public class Orbit(int id) : IAgentModel
{
    public string Name { get; set; } = "ORBIT_" + id;

    public AgentType Type { get; set; } = AgentType.Orbit;
    
    public IDistribution Distribution { get; set; }
}

public abstract class Buffer(int id) : IAgentModel
{
    public string Name { get; set; } = "BUNK_" + id;

    public AgentType Type { get; set; } = AgentType.Buffer;
}

public class QueueBuffer(int id) : Buffer(id);

public class StackBuffer(int id) : Buffer(id);
