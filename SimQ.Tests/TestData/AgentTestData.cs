using SimQ.DAL.Models.ProblemAggregation;
using SimQCore.Modeller.BaseModels;
using SimQCore.Modeller.CustomModels;
using ServiceBlock = SimQ.DAL.Models.ProblemAggregation.ServiceBlock;

namespace SimQ.Tests.TestData;

public class AgentTestData
{
    public List<AgentModel> GetAgentModelData()
    {
        SimpleServiceBlock serviceBlock = new();
        SimpleStackBunker simpleStack = new();
        SimpleSource source1 = new();
        SimpleSource source2 = new();
        SimpleSource source3 = new();
    
        serviceBlock.BindBunker(simpleStack);
    
        List<AgentModel> agents = new();
        agents.Add(source1);
        agents.Add(source2);
        agents.Add(source3);
        agents.Add(serviceBlock);
        agents.Add(simpleStack);
            
        return agents;
    }
    
    public List<Agent> GetAgentData()
    {
        var serviceBlock = new ServiceBlock
        {
            Type = AgentType.ServiceBlock,
            ReflectionType = "SimpleServiceBlock",
            Id = "SBLOCK_0"
        };
        var stack = new Agent
        {
            Type = AgentType.Buffer,
            ReflectionType = "SimpleStackBunker",
            Id = "BUNK_0",
        };
        var source1 = new Agent
        {
            Type = AgentType.Source,
            ReflectionType = "SimpleSource",
            Id = "SRC_0",
        };
        var source2 = new Agent
        {
            Type = AgentType.Source,
            ReflectionType = "SimpleSource",
            Id = "SRC_1",
        };
        var source3 = new Agent
        {
            Type = AgentType.Source,
            ReflectionType = "SimpleSource",
            Id = "SRC_2",
        };
    
        serviceBlock.BindBuffer(stack);
            
        List<Agent> agents = new();
        agents.Add(source1);
        agents.Add(source2);
        agents.Add(source3);
        agents.Add(serviceBlock);
        agents.Add(stack);
            
        return agents;
    }
}