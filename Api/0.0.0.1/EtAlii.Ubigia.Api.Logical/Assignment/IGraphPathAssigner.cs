namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphPathAssigner
    {
        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        Task<INode> Assign(Identifier location, object item, ExecutionScope scope);
    }
}