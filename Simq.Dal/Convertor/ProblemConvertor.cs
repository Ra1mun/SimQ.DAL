using ProblemDto = SimQCore.Modeller.Problem;
using Problem = Simq.Dal.Models.Problem;

namespace Simq.Dal;

public interface IProblemConvertor
{
    Problem Convert(ProblemDto dto);
    ProblemDto Convert(Problem problem);
}

public class ProblemConvertor : IProblemConvertor
{
    private readonly IAgentConverter _agentConverter = new AgentConverter();
    
    public Problem Convert(ProblemDto dto)
    {
        var agents = _agentConverter.Convert(dto.Agents);

        return new Problem
        {
            Agents = agents
        };
    }

    public ProblemDto Convert(Problem problem)
    {
        var agents = _agentConverter.Convert(problem.Agents);
        
        return new ProblemDto
        {
            Agents = agents
        };
    }
}