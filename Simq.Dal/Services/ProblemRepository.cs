using MongoDB.Driver;
using Simq.Dal.Models;
using Problem = Simq.Dal.Models.Problem;

namespace Simq.Dal.Services;

internal interface IProblemRepostiory
{
    Problem FindProblem(string id);
    
    void AddProblem(Problem problem);
    
    bool ExistProblem(string id);
}

public class ProblemRepository : IProblemRepostiory
{
    private readonly IMongoCollection<Problem> _collection;
    
    public ProblemRepository(ProblemDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        _collection = database.GetCollection<Problem>(settings.CollectionName);
    }

    public Problem FindProblem(string id)
    {
        var filter = Builders<Problem>.Filter.Eq(p => p.Id, id);
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public void AddProblem(Problem problem)
    {   
        _collection.InsertOne(problem);
    }

    public bool ExistProblem(string id)
    {
        return _collection.Find(Builders<Problem>.Filter.Eq(p => p.Id, id)).Any();
    }
}