namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    public interface IPropertyCacheRetrieveHandler
    {
        Task<PropertyDictionary> Handle(Identifier identifier, ExecutionScope scope);
    }
}