// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

public interface ILogicalPropertiesSet
{
    PropertyDictionary Get(in Identifier identifier);
    void Store(in Identifier identifier, PropertyDictionary properties);
}
