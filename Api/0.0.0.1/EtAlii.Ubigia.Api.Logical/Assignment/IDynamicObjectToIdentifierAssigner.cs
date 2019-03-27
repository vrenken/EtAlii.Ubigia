namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IDynamicObjectToIdentifierAssigner
    {
        Task<INode> Assign(object o, Identifier id, ExecutionScope scope);
    }
}