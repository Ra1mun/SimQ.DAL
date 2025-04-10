using MongoDB.Driver;
using SimQ.DAL.Models;
using SimQ.DAL.Models.DBSettings;
using SimQ.DAL.Models.ProblemAggregation;

namespace SimQ.DAL.Repository;

internal interface IProblemRepostiory
{
    Problem FindProblem(string id);
    
    string AddProblem(Problem problem);
    
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
        return _collection.Find(p => p.Id == id).FirstOrDefault();
    }

    public string AddProblem(Problem problem)
    {   
        _collection.InsertOne(problem);
        
        return problem.Id;
    }

    public bool ExistProblem(string id)
    {
        return _collection.Find(Builders<Problem>.Filter.Eq(p => p.Id, id)).Any();
    }
}