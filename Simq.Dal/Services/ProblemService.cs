using MongoDB.Driver;
using Simq.Dal.Models;
using ProblemDto = SimQCore.Modeller.Problem;  

namespace Simq.Dal.Services;

public interface IProblemService
{
    ProblemDto GetProblem(string id);
    
    string CreateProblem(ProblemDto problem);
}

public class ProblemService : IProblemService
{
    private readonly IProblemRepostiory _repository;
    private readonly IProblemConvertor _converter = new ProblemConvertor();
    
    public ProblemService(ProblemRepository problemRepository)
    {
        _repository = problemRepository;
    }

    public ProblemDto GetProblem(string id)
    {
        if (!_repository.ExistProblem(id))
        {
            return new ProblemDto();
        }
        
        var problem = _repository.FindProblem(id);
        
        return _converter.Convert(problem);
    }

    public string CreateProblem(ProblemDto problemDto)
    {
        var problem = _converter.Convert(problemDto);
        
        return _repository.AddProblem(problem);
    }
}