using Simq.Dal.Models;
using Simq.Dal.Services;
using SimQ.Tests.Converter;
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
        var agentDtos = AgentConverterTests.GetAgentModelData();

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
        var agentDtos = AgentConverterTests.GetAgentModelData();

        var excepted = new ProblemDto
        {
            Agents = agentDtos
        };

        var actual = _service.GetProblem("67ee76866064def5626ce066");
        
        Assert.Equivalent(excepted, actual);
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