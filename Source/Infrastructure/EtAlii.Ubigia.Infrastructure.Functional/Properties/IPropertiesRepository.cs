// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    public interface IPropertiesRepository
    {
        void Store(in Identifier identifier, PropertyDictionary properties);
        PropertyDictionary Get(in Identifier identifier);
    }
}