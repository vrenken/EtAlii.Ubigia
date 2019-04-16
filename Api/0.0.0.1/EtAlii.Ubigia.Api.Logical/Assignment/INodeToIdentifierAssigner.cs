namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface INodeToIdentifierAssigner 
    {
        Task<INode> Assign(INode node, Identifier id, ExecutionScope scope);
    }
}