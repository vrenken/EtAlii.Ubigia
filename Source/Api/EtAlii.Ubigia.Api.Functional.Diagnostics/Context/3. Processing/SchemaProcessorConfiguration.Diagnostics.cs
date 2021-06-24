// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using EtAlii.xTechnology.Diagnostics;

    public static class SchemaProcessorConfigurationDiagnosticsExtension
    {
        public static SchemaProcessorConfiguration UseFunctionalDiagnostics(this SchemaProcessorConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = Array.Empty<ISchemaProcessorExtension>();
            // var extensions = new ISchemaProcessorExtension[]
            // [
            //     new DiagnosticsQueryProcessorExtension(diagnostics),
            // ]

            return configuration.Use(extensions);

        }
    }
}
