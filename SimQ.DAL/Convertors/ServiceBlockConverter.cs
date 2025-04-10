using SimQ.DAL.Models.ProblemAggregation;
using SimQCore.Modeller.BaseModels;
using Buffer = SimQCore.Modeller.BaseModels.Buffer;
using ServiceBlock = SimQ.DAL.Models.ProblemAggregation.ServiceBlock;
using ServiceBlockDto = SimQCore.Modeller.BaseModels.ServiceBlock;

namespace SimQ.DAL.Convertor;

public interface IServiceBlockConverter
{
    Agent Convert(ServiceBlockDto serviceBlockDto);

    AgentModel Convert(ServiceBlockDto serviceBlockDto, ServiceBlock serviceBlock, Dictionary<string,AgentModel> agentDictionary);
}

public class ServiceBlockConverter : IServiceBlockConverter
{
    public Agent Convert(ServiceBlockDto serviceBlockDto)
    {
        var serviceBlock = new ServiceBlock
        {
            Type = serviceBlockDto.Type,
            ReflectionType = serviceBlockDto.GetType().Name,
            Id = serviceBlockDto.Id,
        };
        
        var bufferDtos = serviceBlockDto.BindedBuffers;
            
        if (!bufferDtos.Any())
        {
            return serviceBlock;
        }
            
        FillBuffers(bufferDtos, serviceBlock);

        return serviceBlock;
    }
    
    public AgentModel Convert(ServiceBlockDto serviceBlockDto, ServiceBlock serviceBlock, Dictionary<string,AgentModel> agentDictionary)
    {
        var buffers = serviceBlock.BindedBuffer;
        if (!buffers.Any())
        {
            return serviceBlockDto;
        }
            
        FillBuffers(serviceBlockDto, buffers, agentDictionary);
            
        return serviceBlockDto;
    }
    
    private static void FillBuffers(List<Buffer> bufferDtos, ServiceBlock serviceBlock)
    {
        var buffers = bufferDtos.Select(bufferDto => new Agent
        {
            Type = bufferDto.Type,
            ReflectionType = bufferDto.GetType().Name,
            Id = bufferDto.Id,
        }).ToList();

        buffers.ForEach(serviceBlock.BindBuffer);
    }
    
    private static void FillBuffers(ServiceBlockDto serviceBlock, List<Agent> buffers, Dictionary<string, AgentModel> agentDictionary)
    {
        foreach (var buffer in buffers)
        {
            if (!agentDictionary.TryGetValue(buffer.ReflectionType, out var bufferDto))
            {
                continue;
            }
            
            serviceBlock.BindBunker(bufferDto as Buffer);
        }
    }
}