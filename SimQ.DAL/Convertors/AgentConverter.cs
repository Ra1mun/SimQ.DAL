using System.Reflection;
using SimQ.DAL.Models.ProblemAggregation;
using SimQCore.Modeller.BaseModels;
using ServiceBlock = SimQ.DAL.Models.ProblemAggregation.ServiceBlock;
using ServiceBlockDto = SimQCore.Modeller.BaseModels.ServiceBlock;

namespace SimQ.DAL.Convertor;

public interface IAgentConverter
{ 
    List<AgentModel> ConvertMany(List<Agent> agents);
    
    List<Agent> ConvertMany(List<AgentModel> agents);
    
    AgentModel Convert(Agent agent);
    
    Agent Convert(AgentModel model);
}

public class AgentConverter : IAgentConverter
{
    private readonly Dictionary<string,AgentModel> _agentDictionary;
    private readonly IServiceBlockConverter _serviceBlockConverter;

    public AgentConverter()
    {
        _agentDictionary = GetAgentsDictionary();
        _serviceBlockConverter = new ServiceBlockConverter();
    }

    public List<AgentModel> ConvertMany(List<Agent> agents)
    {
        return agents.Select(Convert).ToList();
    }

    public List<Agent> ConvertMany(List<AgentModel> agents)
    {
        return agents.Select(Convert).ToList();
    }

    public Agent Convert(AgentModel agentModel)
    {
        if (agentModel is ServiceBlockDto serviceBlockDto)
        {
            return _serviceBlockConverter.Convert(serviceBlockDto);
        }

        return new Agent
        {
            Type = agentModel.Type,
            ReflectionType = agentModel.GetType().Name,
            Id = agentModel.Id,
        };
    }
    
    public AgentModel Convert(Agent agent)
    {
        if (!_agentDictionary.TryGetValue(agent.ReflectionType, out var agentModel))
        {
            throw new ArgumentException($"Agent {agent.ReflectionType} does not exist");
        }
        
        if (agent is ServiceBlock serviceBlock && agentModel is ServiceBlockDto serviceBlockDto)
        {
            return _serviceBlockConverter.Convert(serviceBlockDto, serviceBlock, _agentDictionary);
        }

        return agentModel;
    }

    private static Dictionary<string, AgentModel> GetAgentsDictionary()
    {
        var baseType = typeof(AgentModel);
        var assembly = Assembly.GetAssembly(baseType);
        if (assembly == null)
        {
            throw new ApplicationException("Unable to load assembly " + baseType.FullName);
        }
            
        var agentTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(baseType));
            
        var userAgents = IgnoreBaseTypes(agentTypes);
        
        var agents = userAgents.Select(userAgent => Activator.CreateInstance(userAgent) as AgentModel).ToList();
        
        return agents.ToDictionary(agent => agent.GetType().Name);
    }
    
    private static IEnumerable<Type> IgnoreBaseTypes(IEnumerable<Type> types)
    {
        return types.Where(type => type.IsClass && !type.IsAbstract);
    }
}