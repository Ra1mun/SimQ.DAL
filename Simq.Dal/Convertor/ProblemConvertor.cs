using System.Diagnostics;
using Simq.Dal.Models;
using SimQCore.Modeller.BaseModels;
using ProblemDto = SimQCore.Modeller.Problem;
using Problem = Simq.Dal.Models.Problem;
using AgentTypeDto = SimQCore.Modeller.BaseModels.AgentType;
using AgentType = Simq.Dal.Models.AgentType;
using Source = Simq.Dal.Models.Source;

namespace Simq.Dal;

internal interface IProblemConvertor
{
    Problem Convert(ProblemDto dto);
    ProblemDto Convert(Problem problem);
}

public class ProblemConvertor : IProblemConvertor
{
    public Problem Convert(ProblemDto dto)
    {
        return new Problem
        {
            Agents = 
        }
    }

    public ProblemDto Convert(Problem problem)
    {
        var agents = ConvertAgents(problem.Agents);

        return new ProblemDto
        {
            Agents = agents
        };
    }
    
    
    private List<IAgentModel> ConvertAgents(List<AgentModel> agents)
    {
        var result = new List<IAgentModel>();
        foreach (var agent in agents)
        {
            IAgentModel agentModel;
            
        }
    }
}