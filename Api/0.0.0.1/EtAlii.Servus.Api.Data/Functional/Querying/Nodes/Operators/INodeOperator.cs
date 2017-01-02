namespace EtAlii.Servus.Api.Data
{
    using Remotion.Linq;

    public interface INodeOperator
    {
        void Operate(QueryModel queryModel, int index, ScriptAggregator scriptAggregator);
    }
}
