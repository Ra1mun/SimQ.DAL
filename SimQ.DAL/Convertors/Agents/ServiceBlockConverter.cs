using SimQ.DAL.Extensions;
using SimQ.DAL.Models.ProblemAggregation;
using SimQCore.Modeller.BaseModels;
using BufferDto = SimQCore.Modeller.BaseModels.Buffer;
using Buffer = SimQ.DAL.Models.ProblemAggregation.Buffer;
using ServiceBlock = SimQ.DAL.Models.ProblemAggregation.ServiceBlock;
using ServiceBlockDto = SimQCore.Modeller.BaseModels.ServiceBlock;

namespace SimQ.DAL.Convertor;

public interface IServiceBlockConverter
{
    ServiceBlock Convert(AgentModel agentModel);

    ServiceBlockDto Convert(AgentModel agentModel, ServiceBlock serviceBlock);
}

public class ServiceBlockConverter : IServiceBlockConverter
{
    private readonly IBufferConverter _bufferConverter = new BufferConverter();
    public ServiceBlock Convert(AgentModel agentModel)
    {
        var serviceBlockDto = agentModel as ServiceBlockDto;
        
        var serviceBlock = new ServiceBlock
        {
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

    public ServiceBlockDto Convert(AgentModel agentModel, ServiceBlock serviceBlock)
    {
        var serviceBlockDto = agentModel as ServiceBlockDto;
        
        var buffers = serviceBlock.BindedBuffer;
        if (!buffers.Any())
        {
            return serviceBlockDto;
        }
            
        FillBuffers(serviceBlockDto, buffers);
            
        return serviceBlockDto;
    }
    
    private void FillBuffers(List<BufferDto> bufferDtos, ServiceBlock serviceBlock)
    {
        var buffers = bufferDtos.Select(bufferDto => _bufferConverter.Convert(bufferDto)).ToList();

        buffers.ForEach(serviceBlock.BindBuffer);
    }
    
    private static void FillBuffers(ServiceBlockDto serviceBlock, List<Agent> buffers)
    {
        foreach (var buffer in buffers)
        {
            if (!ConverterExtensions.TryToGetAgentModel(buffer.ReflectionType, out var bufferDto))
            {
                continue;
            }
            
            serviceBlock.BindBunker(bufferDto as BufferDto);
        }
    }
}