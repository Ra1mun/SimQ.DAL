using SimQ.DAL.Models.DBSettings;
using SimQ.DAL.Repository;
using SimQ.DAL.Services;
using SimQCore.Modeller;
using SimQCore.Modeller.BaseModels;
using SimQCore.Modeller.CustomModels;

var problem = SetupProblem();
var service = SetupService();

var id = service.InsertProblem(problem);

Console.WriteLine($"Created problem with id {id}");
return;


IProblemService SetupService()
{
    var settings = new ProblemDatabaseSettings
    {
        ConnectionString = "mongodb://localhost:27017",
        DatabaseName = "SimQ",
        CollectionName = "Problems"
    };

    var problemRepository = new ProblemRepository(settings);

    return new ProblemService(problemRepository);
}

Problem SetupProblem()
{
    ExponentialServiceBlock serviceBlock = new();
    SimpleStackBunker simpleStack = new();
    BernoulliSource source1 = new();
    BetaSource source2 = new();

    serviceBlock.BindBunker(simpleStack);

    List<AgentModel> agentModels =
    [
        source1,
        source2,
        serviceBlock,
        simpleStack
    ];

    return new Problem { Agents = agentModels };
}