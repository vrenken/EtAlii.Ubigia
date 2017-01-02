namespace EtAlii.Servus.Api.Fabric
{
    using System.Collections.Generic;

    public interface IPropertyCacheProvider
    {
        IDictionary<Identifier, PropertyDictionary> Cache { get; }
    }
}