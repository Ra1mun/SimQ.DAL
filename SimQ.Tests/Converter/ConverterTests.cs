using Simq.Dal;
using Simq.Dal.Models;
using SimQCore.Modeller.BaseModels;
using SimQCore.Modeller.CustomModels;
using Xunit;
using Assert = NUnit.Framework.Assert;
using ServiceBlock = Simq.Dal.Models.ServiceBlock;

namespace SimQ.Tests.Converter;

public class ConverterTests
{
    [Fact]
    public void Convert_When_Agents_Given_Returns_AgentModels()
    {
        //Arrange
        var agents = GetAgentData();
        var excepted = GetAgentModelData();

        var converter = new AgentConverter();
        
        //Act 
        var actual = converter.Convert(agents);
        
        //Assert 
        Assert.AreEqual(excepted, actual);
    }
    
    [Fact]
    public void Convert_When_AgentModels_Given_Returns_Agents()
    {
        //Arrange
        var excepted = GetAgentData();
        var agentDto = GetAgentModelData();

        var converter = new AgentConverter();
        
        //Act 
        var actual = converter.Convert(agentDto);
        
        //Assert 
        Assert.AreEqual(excepted, actual);
    }
    
    private static List<AgentModel> GetAgentModelData()
    {
        SimpleServiceBlock serviceBlock = new();
        SimpleStackBunker simpleStack = new();
        SimpleSource source1 = new();
        SimpleSource source2 = new();

        serviceBlock.BindBunker(simpleStack);

        List<AgentModel> agents = new();
        agents.Add(source1);
        agents.Add(source2);
        agents.Add(serviceBlock);
        agents.Add(simpleStack);
        
        return agents;
    }

    private static List<Agent> GetAgentData()
    {
        var serviceBlock = new ServiceBlock
        {
            Type = AgentType.ServiceBlock,
            ReflectionType = "SimpleServiceBlock"
        };
        var stack = new Agent
        {
            Type = AgentType.Buffer,
            ReflectionType = "SimpleStackBunker"
        };
        var source1 = new Agent
        {
            Type = AgentType.Source,
            ReflectionType = "SimpleSource"
        };
        var source2 = new Agent
        {
            Type = AgentType.Source,
            ReflectionType = "SimpleSource"
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