namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Threading.Tasks;

    public interface IPropertyCacheStoreHandler
    {
        Task Handle(Identifier identifier);
        Task Handle(Identifier identifier, PropertyDictionary properties, ExecutionScope scope);
    }
}