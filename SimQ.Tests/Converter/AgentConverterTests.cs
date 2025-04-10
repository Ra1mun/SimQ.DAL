using SimQ.DAL;
using SimQ.DAL.Convertor;
using SimQ.DAL.Models;
using SimQ.DAL.Models.ProblemAggregation;
using SimQ.Tests.TestData;
using SimQCore.Modeller.BaseModels;
using SimQCore.Modeller.CustomModels;
using ServiceBlock = SimQ.DAL.Models.ProblemAggregation.ServiceBlock;

namespace SimQ.Tests.Converter;

public class AgentConverterTests
{
    [Fact]
    public void Convert_When_Agents_Given_Returns_AgentModels()
    {
        //Arrange
        var data = new AgentTestData();
        
        var expected = data.GetAgentModelData();
        var agents = data.GetAgentData();
        
        var converter = new AgentConverter();
        
        //Act 
        var actual = converter.ConvertMany(agents);
        
        //Assert 
        Assert.Equivalent(expected, actual);
    }
    
    [Fact]
    public void Convert_When_AgentModels_Given_Returns_Agents()
    {
        //Arrange
        var data = new AgentTestData();
        
        var agentDtos = data.GetAgentModelData();
        var expected = data.GetAgentData();
        
        var converter = new AgentConverter();
        
        //Act 
        var actual = converter.ConvertMany(agentDtos);
        
        //Assert 
        Assert.Equal(expected.Count, actual.Count);
    }
}