namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface ITraversalContextPropertySet
    {
        Task<PropertyDictionary> Retrieve(Identifier entryIdentifier, ExecutionScope scope);
    }
}