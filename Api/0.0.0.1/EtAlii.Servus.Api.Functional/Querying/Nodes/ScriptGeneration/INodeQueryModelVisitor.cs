namespace EtAlii.Servus.Api.Functional
{
    using Remotion.Linq;

    public interface INodeQueryModelVisitor : IQueryModelVisitor
    {
        ResultOperator ResultOperator { get; }
        string GetScriptText();
    }
}