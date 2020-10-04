namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IConstantToIdentifierTagAssigner
    {
        Task<INode> Assign(string constant, Identifier id, ExecutionScope scope);
    }
}