namespace EtAlii.Ubigia.Api.Fabric
{
    using System.Collections.Generic;

    public interface IPropertyCacheProvider
    {
        IDictionary<Identifier, PropertyDictionary> Cache { get; }
    }
}