using Moq;
using SimQ.DAL.Models.ProblemAggregation;
using SimQ.DAL.Repository;
using SimQ.DAL.Services;
using SimQ.Tests.TestData;
using ProblemDto = SimQCore.Modeller.Problem;

namespace SimQ.Tests.Services;

public class ProblemServiceTests
{
    private readonly IProblemService _service;
    private readonly Mock<IProblemRepository> _repositoryMock;
    private readonly AgentTestData _testData;
    
    public ProblemServiceTests()
    {
        _repositoryMock = new Mock<IProblemRepository>(MockBehavior.Strict);
        _service = new ProblemService(_repositoryMock.Object);
        _testData = new AgentTestData();
    }
    
    [Fact]
    public void CreateProblem_When_Correct_Problem_Given_Returns_Id()
    {
        // Arrange
        var agentDtos = _testData.GetAgentModelData();
        var problemDto = new ProblemDto
        {
            Agents = agentDtos,
        };
        
        var expectedId = "test_id_123";
        _repositoryMock.Setup(r => r.AddProblem(It.IsAny<Problem>()))
            .Returns(expectedId);
        
        // Act
        var actualId = _service.InsertProblem(problemDto);
        
        // Assert
        Assert.Equal(expectedId, actualId);
        _repositoryMock.Verify(r => r.AddProblem(It.IsAny<Problem>()), Times.Once);
    }
    
    
    [Fact]
    public void GetProblem_When_Problem_Exists_Returns_Problem()
    {
        // Arrange
        var agentModels = _testData.GetAgentModelData();
        var expectedProblem = new ProblemDto
        {
            Agents = agentModels
        };
        var agents = _testData.GetAgentData();
        
        const string problemId = "test_id_123";
        _repositoryMock.Setup(r => r.ExistProblem(problemId))
            .Returns(true);
        _repositoryMock.Setup(r => r.FindProblem(problemId))
            .Returns(new Problem { Id = problemId, Agents = agents });
        
        // Act
        var actualProblem = _service.GetProblem(problemId);

        // Assert
        Assert.Equal(expectedProblem.Agents.Count, actualProblem.Agents.Count);
        _repositoryMock.Verify(r => r.ExistProblem(problemId), Times.Once);
        _repositoryMock.Verify(r => r.FindProblem(problemId), Times.Once);
    }
    
    [Fact]
    public void GetProblem_When_Problem_Not_Exists_Returns_Empty_Problem()
    {
        // Arrange
        var problemId = "non_existent_id";
        _repositoryMock.Setup(r => r.ExistProblem(problemId))
            .Returns(false);
        
        // Act
        var actualProblem = _service.GetProblem(problemId);

        // Assert
        Assert.Null(actualProblem.Agents);
        _repositoryMock.Verify(r => r.ExistProblem(problemId), Times.Once);
        _repositoryMock.Verify(r => r.FindProblem(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void UpdateProblem_When_Problem_Exists_Returns_True()
    {
        // Arrange
        var problemId = "test_id_123";
        var agentDtos = _testData.GetAgentModelData();
        var problemDto = new ProblemDto
        {
            Agents = agentDtos
        };

        _repositoryMock.Setup(r => r.ExistProblem(problemId))
            .Returns(true);
        _repositoryMock.Setup(r => r.TryToEditProblem(problemId, It.IsAny<Problem>()))
            .Returns(true);

        // Act
        var result = _service.UpdateProblem(problemId, problemDto);

        // Assert
        Assert.True(result);
        _repositoryMock.Verify(r => r.ExistProblem(problemId), Times.Once);
        _repositoryMock.Verify(r => r.TryToEditProblem(problemId, It.IsAny<Problem>()), Times.Once);
    }

    [Fact]
    public void UpdateProblem_When_Problem_Not_Exists_Returns_False()
    {
        // Arrange
        var problemId = "non_existent_id";
        var agentDtos = _testData.GetAgentModelData();
        var problemDto = new ProblemDto
        {
            Agents = agentDtos
        };

        _repositoryMock.Setup(r => r.ExistProblem(problemId))
            .Returns(false);

        // Act
        var result = _service.UpdateProblem(problemId, problemDto);

        // Assert
        Assert.False(result);
        _repositoryMock.Verify(r => r.ExistProblem(problemId), Times.Once);
        _repositoryMock.Verify(r => r.TryToEditProblem(It.IsAny<string>(), It.IsAny<Problem>()), Times.Never);
    }

    [Fact]
    public void UpdateProblem_When_Repository_Update_Fails_Returns_False()
    {
        // Arrange
        var problemId = "test_id_123";
        var agentDtos = _testData.GetAgentModelData();
        var problemDto = new ProblemDto
        {
            Agents = agentDtos
        };

        _repositoryMock.Setup(r => r.ExistProblem(problemId))
            .Returns(true);
        _repositoryMock.Setup(r => r.TryToEditProblem(problemId, It.IsAny<Problem>()))
            .Returns(false);

        // Act
        var result = _service.UpdateProblem(problemId, problemDto);

        // Assert
        Assert.False(result);
        _repositoryMock.Verify(r => r.ExistProblem(problemId), Times.Once);
        _repositoryMock.Verify(r => r.TryToEditProblem(problemId, It.IsAny<Problem>()), Times.Once);
    }
}