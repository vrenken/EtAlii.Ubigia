namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using Remotion.Linq;

    public interface INodeQueryModelVisitor : IQueryModelVisitor
    {
        ResultOperator ResultOperator { get; }
        string GetScriptText();
    }
}