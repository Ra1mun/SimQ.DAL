using SimQ.DAL.Models.ProblemAggregation;
using SimQCore.Modeller.BaseModels;
using ServiceBlock = SimQ.DAL.Models.ProblemAggregation.ServiceBlock;
using Source = SimQ.DAL.Models.ProblemAggregation.Source;

namespace SimQ.DAL.Convertor;

public interface ISourceConverter
{
    Source Convert(AgentModel agentModel);
}

public class SourceConverter : ISourceConverter
{
    public Source Convert(AgentModel agentModel)
    {
        return new Source
        {
            ReflectionType = agentModel.GetType().Name,
            Id = agentModel.Id
        };
    }
}