namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using Remotion.Linq;

    public interface INodeOperator
    {
        void Operate(QueryModel queryModel, int index, IScriptAggregator scriptAggregator);
    }
}
