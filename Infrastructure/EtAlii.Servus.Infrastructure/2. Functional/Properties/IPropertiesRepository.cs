namespace EtAlii.Servus.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public interface IPropertiesRepository
    {
        void Store(Identifier identifier, PropertyDictionary properties);
        PropertyDictionary Get(Identifier identifier);
    }
}