using System.Reflection;
using SimQCore.Modeller.BaseModels;

namespace SimQ.DAL.Extensions;

public static class ConverterExtensions
{
    private static Dictionary<string, AgentModel> _agentDictionary = new();
    
    public static bool TryToGetAgentModel(string reflectionType, out AgentModel agentModel)
    {
        if (_agentDictionary.Count == 0)
        {
            _agentDictionary = GetAgentsDictionary();
        }
        
        return _agentDictionary.TryGetValue(reflectionType, out agentModel);
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