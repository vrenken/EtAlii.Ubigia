// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Threading.Tasks;

public interface IPropertyCacheStoreHandler
{
    Task Handle(Identifier identifier);
    Task Handle(Identifier identifier, PropertyDictionary properties, ExecutionScope scope);
}
