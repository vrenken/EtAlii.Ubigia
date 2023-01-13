// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Collections.Generic;

public interface IPropertyCacheProvider
{
    IDictionary<Identifier, PropertyDictionary> Cache { get; }
}
