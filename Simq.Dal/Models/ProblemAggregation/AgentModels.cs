using SimQCore.Modeller.BaseModels;

namespace Simq.Dal.Models;

public class Agent
{
    public AgentType Type { get; set; }
    
    public string ReflectionType { get; set; }

    //List<string> LinkedAgents { get; set; }
}

public class ServiceBlock : Agent
{
    public List<Agent> BindedBuffer { get; } = new();

    public void BindBuffer(Agent agent)
    {
        if (agent.Type != AgentType.Buffer)
        {
            return;
        }
        
        BindedBuffer.Add(agent);
    }
}

