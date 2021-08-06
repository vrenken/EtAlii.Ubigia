// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Microsoft.Extensions.Configuration;

    public interface ISchemaProcessorOptions : IExtensible
    {
        IConfigurationRoot ConfigurationRoot { get; }

        ISchemaScope SchemaScope { get; }

        ITraversalContext TraversalContext { get; }

        SchemaProcessorOptions Use(ISchemaScope scope);

        SchemaProcessorOptions Use(ITraversalContext traversalContext);
    }
}
