namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Threading.Tasks;

    public interface IPropertiesContext
    {
        Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope);
        Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope);

        event Action<Identifier> Stored;
    }
}
