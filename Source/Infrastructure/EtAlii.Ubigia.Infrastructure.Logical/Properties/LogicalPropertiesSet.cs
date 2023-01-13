// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using EtAlii.Ubigia.Infrastructure.Fabric;

public class LogicalPropertiesSet : ILogicalPropertiesSet
{
    private readonly IFabricContext _fabricContext;

    public LogicalPropertiesSet(IFabricContext fabricContext)
    {
        _fabricContext = fabricContext;
    }

    public PropertyDictionary Get(in Identifier identifier)
    {
        return _fabricContext.Properties.Get(identifier);
    }

    public void Store(in Identifier identifier, PropertyDictionary properties)
    {
        _fabricContext.Properties.Store(identifier, properties);
    }
}
