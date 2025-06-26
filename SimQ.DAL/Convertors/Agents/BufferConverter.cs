using SimQ.DAL.Models.ProblemAggregation;
using SimQCore.Modeller.BaseModels;
using Buffer = SimQ.DAL.Models.ProblemAggregation.Buffer;

namespace SimQ.DAL.Convertor;

public interface IBufferConverter
{
    Buffer Convert(AgentModel agentModel);
}

public class BufferConverter : IBufferConverter
{
    public Buffer Convert(AgentModel agentModel)
    {
        return new Buffer
        {
            ReflectionType = agentModel.GetType().Name,
            Id = agentModel.Id
        };
    }
}