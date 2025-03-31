using MongoDB.Driver;
using Simq.Dal.Models;
using ProblemDto = SimQCore.Modeller.Problem;  

namespace Simq.Dal.Services;

internal interface IProblemService
{
    ProblemDto GetProblem(string id);
    
    void CreateProblem(ProblemDto problem);
}

public class ProblemService : IProblemService
{
    private readonly IProblemRepostiory _repository;
    
    public ProblemService(ProblemRepository problemRepository)
    {
        _repository = problemRepository;
    }

    public ProblemDto GetProblem(string id)
    {
        var problem = _repository.FindProblem(id);
        
        return new ProblemDto();
    }

    public void CreateProblem(ProblemDto problem)
    {
        throw new NotImplementedException();
    }
}