using MongoDB.Driver;
using Simq.Dal.Models;

namespace Simq.Dal.Services;

public class ProblemService
{
    private readonly IMongoCollection<Problem> _problemRepository;
    
    public ProblemService(ProblemDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        _problemRepository = database.GetCollection<Problem>(settings.ProblemCollectionName);
    }

    public Problem Get(string id)
    {
        var findProblems = _problemRepository.Find(problem => problem.Id == id);
        return findProblems.FirstOrDefault();
    }
}