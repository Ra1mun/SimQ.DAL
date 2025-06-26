using SimQ.DAL.Models.ProblemAggregation;
using SimQCore.Modeller.BaseModels;
using SimQCore.Modeller.CustomModels;
using Buffer = SimQ.DAL.Models.ProblemAggregation.Buffer;
using ServiceBlock = SimQ.DAL.Models.ProblemAggregation.ServiceBlock;
using Source = SimQ.DAL.Models.ProblemAggregation.Source;

namespace SimQ.Tests.TestData;

public class AgentTestData
{
    public List<AgentModel> GetAgentModelData()
    {
        ExponentialServiceBlock serviceBlock = new();
        SimpleStackBunker simpleStack = new();
        SimpleSource source1 = new();
        SimpleSource source2 = new();
    
        serviceBlock.BindBunker(simpleStack);
    
        List<AgentModel> agents =
        [
            source1,
            source2,
            serviceBlock,
            simpleStack
        ];

        return agents;
    }
    
    public List<Agent> GetAgentData()
    {
        var serviceBlock = new ServiceBlock
        {
            ReflectionType = "ExponentialServiceBlock",
            Id = "SBLOCK_0"
        };
        var stack = new Buffer
        {
            ReflectionType = "SimpleStackBunker",
            Id = "BUNK_0",
        };
        var source1 = new Source
        {
            ReflectionType = "SimpleSource",
            Id = "SRC_0",
        };
        var source2 = new Source
        {
            ReflectionType = "SimpleSource",
            Id = "SRC_1",
        };
    
        serviceBlock.BindBuffer(stack);
            
        List<Agent> agents = new();
        agents.Add(source1);
        agents.Add(source2);
        agents.Add(serviceBlock);
        agents.Add(stack);
            
        return agents;
    }
}