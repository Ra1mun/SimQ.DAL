using SimQ.DAL.Models;
using SimQ.DAL.Models.DBSettings;
using SimQ.DAL.Repository;
using SimQ.DAL.Services;
using SimQ.Tests.Converter;
using SimQ.Tests.TestData;
using ProblemDto = SimQCore.Modeller.Problem;

namespace SimQ.Tests.Services;

public class ProblemServiceTests
{
    private readonly IProblemService _service;
    
    public ProblemServiceTests()
    {
        _service = SetupService();
    }
    
    [Fact]
    public void CreateProblem_When_Correct_Problem_Given_Returns_Id()
    {
        var data = new AgentTestData();
        
        var agentDtos = data.GetAgentModelData();

        var problemDto = new ProblemDto
        {
            Agents = agentDtos,
        };
        
        var id = _service.CreateProblem(problemDto);
        
        Assert.NotNull(id);
    }
    
    [Fact]
    public void GetProblem_When_Correct_Id_Given()
    {
        var data = new AgentTestData();
        
        var agentDtos = data.GetAgentModelData();

        var excepted = new ProblemDto
        {
            Agents = agentDtos
        };

        var actual = _service.GetProblem("67f29b96875fcefa1fdcfbdc");
        
        Assert.Equal(excepted.Agents.Count, actual.Agents.Count);
    }

    private IProblemService SetupService()
    {
        var settings = new ProblemDatabaseSettings
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "SimQ",
            CollectionName = "Problems",
        };
        
        var repository = new ProblemRepository(settings);
        
        return new ProblemService(repository);
    }
}