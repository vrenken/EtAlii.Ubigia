namespace EtAlii.Servus.Api.Functional
{
    using Remotion.Linq;

    public interface INodeOperator
    {
        void Operate(QueryModel queryModel, int index, IScriptAggregator scriptAggregator);
    }
}
