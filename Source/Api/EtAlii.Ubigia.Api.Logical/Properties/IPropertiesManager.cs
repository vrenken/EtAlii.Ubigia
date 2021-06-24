// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    /// <summary>
    /// Facade that hides away complex logical Property operations.
    /// </summary>
    public interface IPropertiesManager
    {
        Task<PropertyDictionary> Get(Identifier identifier, ExecutionScope scope);

        Task Set(Identifier identifier,  PropertyDictionary properties, ExecutionScope scope);

        Task<bool> HasProperties(Identifier identifier, ExecutionScope scope);
    }
}