using System.Reflection;
using Simq.Dal.Models;
using SimQCore.Modeller.BaseModels;
using Buffer = SimQCore.Modeller.BaseModels.Buffer;
using ServiceBlock = Simq.Dal.Models.ServiceBlock;
using ServiceBlockDto = SimQCore.Modeller.BaseModels.ServiceBlock;

namespace Simq.Dal;

public interface IAgentConverter
{
    List<AgentModel> Convert(List<Agent> agents);
    
    List<Agent> Convert(List<AgentModel> agents);
}

public class AgentConverter : IAgentConverter
{
    public List<AgentModel> Convert(List<Agent> agents)
    {
        var agentsDictionary = GetAgentsDictionary();
        
        return agents.Select(agent => ConvertAgent(agent, agentsDictionary)).ToList();
    }

    public List<Agent> Convert(List<AgentModel> agents)
    {
        return agents.Select(ConvertAgentModel).ToList();
    }

    private static Agent ConvertAgentModel(AgentModel agentModel)
    {
        if (agentModel is ServiceBlockDto serviceBlockDto)
        {
            var serviceBlock = new ServiceBlock()
            {
                Type = serviceBlockDto.Type,
                ReflectionType = serviceBlockDto.GetType().Name
            };
            var bufferDtos = serviceBlockDto.BindedBuffers;
            
            if (!bufferDtos.Any())
            {
                return serviceBlock;
            }
            
            var buffers = bufferDtos.Select(bufferDto => new Agent
            {
                Type = bufferDto.Type,
                ReflectionType = bufferDto.GetType().Name
            }).ToList();

            buffers.ForEach(serviceBlock.BindBuffer);
            
            return serviceBlock;
        }

        return new Agent
        {
            Type = agentModel.Type,
            ReflectionType = agentModel.GetType().Name
        };
    }

    private static AgentModel ConvertAgent(Agent agent, Dictionary<string, AgentModel> agentsDictionary)
    {
        if (!agentsDictionary.TryGetValue(agent.ReflectionType, out var agentModel))
        {
            throw new ArgumentException($"Agent {agent.ReflectionType} does not exist");
        }

        if (agent is ServiceBlock serviceBlock)
        {
            var serviceBlockDto = agentModel as ServiceBlockDto;
            var buffers = serviceBlock.BindedBuffer;
            if (!buffers.Any())
            {
                return agentModel;
            }
            
            FillBindedBuffers(serviceBlockDto, buffers, agentsDictionary);
        }

        return agentModel;
    }

    private static void FillBindedBuffers(ServiceBlockDto serviceBlock, List<Agent> buffers, Dictionary<string, AgentModel> agentsDictionary)
    {
        foreach (var buffer in buffers)
        {
            if (!agentsDictionary.TryGetValue(buffer.ReflectionType, out var bufferDto))
            {
                continue;
            }
                    
            var instance = bufferDto as Buffer;
                    
            serviceBlock.BindBunker(instance);
        }
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