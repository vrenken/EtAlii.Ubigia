// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Threading.Tasks;

public interface IPropertiesContext
{
    Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope);
    Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope);
}
