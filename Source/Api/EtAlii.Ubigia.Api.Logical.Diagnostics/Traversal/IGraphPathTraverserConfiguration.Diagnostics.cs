// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using Microsoft.Extensions.Configuration;

    public static class GraphPathTraverserConfigurationDiagnosticsExtension
    {
        public static GraphPathTraverserConfiguration UseLogicalDiagnostics(this GraphPathTraverserConfiguration configuration, IConfigurationRoot configurationRoot)
        {
            var extensions = new IGraphPathTraverserExtension[]
            {
                new DiagnosticsGraphPathTraverserExtension(configurationRoot),
            };

            return configuration.Use(extensions);
        }
    }
}
