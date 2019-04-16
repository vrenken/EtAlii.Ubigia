namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IPropertiesToIdentifierAssigner
    {
        Task<INode> Assign(IPropertyDictionary properties, Identifier id, ExecutionScope scope);
    }
}