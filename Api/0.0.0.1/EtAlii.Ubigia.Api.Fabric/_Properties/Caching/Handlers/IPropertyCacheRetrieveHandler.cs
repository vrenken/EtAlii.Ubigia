namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    internal interface IPropertyCacheRetrieveHandler
    {
        Task<PropertyDictionary> Handle(Identifier identifier, ExecutionScope scope);
    }
}