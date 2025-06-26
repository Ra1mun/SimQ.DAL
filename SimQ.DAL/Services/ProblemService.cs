using SimQ.DAL.Convertor;
using SimQ.DAL.Repository;
using ProblemDto = SimQCore.Modeller.Problem;  

namespace SimQ.DAL.Services;

public interface IProblemService
{
    ProblemDto GetProblem(string id);
    
    string InsertProblem(ProblemDto problem);
    
    bool UpdateProblem(string id, ProblemDto problem);
}

public class ProblemService : IProblemService
{
    private readonly IProblemRepository _repository;
    private readonly IProblemConvertor _converter = new ProblemConvertor();
    
    public ProblemService(IProblemRepository problemRepository)
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

    public string InsertProblem(ProblemDto problemDto)
    {
        var problem = _converter.Convert(problemDto);
        
        return _repository.AddProblem(problem);
    }

    public bool UpdateProblem(string id, ProblemDto problemDto)
    {
        if (!_repository.ExistProblem(id))
            return false;
        
        var problem = _converter.Convert(problemDto);
        
        return _repository.TryToEditProblem(id, problem);
    }
}