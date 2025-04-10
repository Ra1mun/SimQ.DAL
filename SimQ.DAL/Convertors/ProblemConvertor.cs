using Models_ProblemAggregation_Problem = SimQ.DAL.Models.ProblemAggregation.Problem;
using ProblemDto = SimQCore.Modeller.Problem;
using Problem = SimQ.DAL.Models.ProblemAggregation.Problem;
using ProblemAggregation_Problem = SimQ.DAL.Models.ProblemAggregation.Problem;

namespace SimQ.DAL.Convertor;

public interface IProblemConvertor
{
    Models_ProblemAggregation_Problem Convert(ProblemDto dto);
    ProblemDto Convert(Models_ProblemAggregation_Problem problem);
}

public class ProblemConvertor : IProblemConvertor
{
    private readonly IAgentConverter _agentConverter = new AgentConverter();
    
    public Models_ProblemAggregation_Problem Convert(ProblemDto dto)
    {
        var agents = _agentConverter.ConvertMany(dto.Agents);

        return new Models_ProblemAggregation_Problem
        {
            Agents = agents
        };
    }

    public ProblemDto Convert(Models_ProblemAggregation_Problem problem)
    {
        var agents = _agentConverter.ConvertMany(problem.Agents);
        
        return new ProblemDto
        {
            Agents = agents
        };
    }
}