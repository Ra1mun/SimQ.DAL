namespace Simq.Dal.Models;

public interface IAgentModel
{ 
    string Name { get; set; }
    
    AgentType Type { get; set; }
    
    List<string> LinkedAgents { get; set; }
}

public enum AgentType
{
    Source,
    ServiceBlock,
    Buffer,
    Call
}