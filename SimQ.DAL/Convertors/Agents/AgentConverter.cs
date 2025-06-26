using System.Reflection;
using SimQ.DAL.Extensions;
using SimQ.DAL.Models.ProblemAggregation;
using SimQCore.Modeller.BaseModels;
using ServiceBlock = SimQ.DAL.Models.ProblemAggregation.ServiceBlock;
using ServiceBlockDto = SimQCore.Modeller.BaseModels.ServiceBlock;
using Source = SimQ.DAL.Models.ProblemAggregation.Source;
using SourceDto = SimQCore.Modeller.BaseModels.Source;
using Buffer = SimQ.DAL.Models.ProblemAggregation.Buffer;
using BufferDto = SimQCore.Modeller.BaseModels.Buffer;

namespace SimQ.DAL.Convertor;

public interface IAgentConverter
{ 
    List<AgentModel> ConvertMany(List<Agent> agents);
    
    List<Agent> ConvertMany(List<AgentModel> agents);
}

public class AgentConverter : IAgentConverter
{
    private readonly IServiceBlockConverter _serviceBlockConverter = new ServiceBlockConverter();
    private readonly ISourceConverter _sourceConverter = new SourceConverter();
    private readonly IBufferConverter _bufferConverter = new BufferConverter();

    public List<AgentModel> ConvertMany(List<Agent> agents)
    {
        if(agents == null)
            return [];
        
        return agents.Select(Convert).ToList();
    }

    public List<Agent> ConvertMany(List<AgentModel> agents)
    {
        return agents.Select(Convert).ToList();
    }

    private Agent Convert(AgentModel agentModel)
    {
        return agentModel switch
        {
            ServiceBlockDto serviceBlockDto => _serviceBlockConverter.Convert(serviceBlockDto),
            SourceDto sourceDto => _sourceConverter.Convert(sourceDto),
            BufferDto bufferDto => _bufferConverter.Convert(bufferDto),
            _ => throw new Exception("Unknown agent type")
        };
    }
    
    private AgentModel Convert(Agent agent)
    {
        if (!ConverterExtensions.TryToGetAgentModel(agent.ReflectionType, out var agentModel))
        {
            throw new ArgumentException($"Agent {agent.ReflectionType} does not exist");
        }
        
        if (agent is ServiceBlock serviceBlock)
        {
            return _serviceBlockConverter.Convert(agentModel, serviceBlock);
        }

        return agentModel;
    }
}