// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    public static class GraphPathTraverserConfigurationDiagnosticsExtension
    {
        public static GraphPathTraverserConfiguration UseLogicalDiagnostics(this GraphPathTraverserConfiguration configuration)
        {
            var extensions = new IGraphPathTraverserExtension[]
            {
                new DiagnosticsGraphPathTraverserExtension(),
            };

            return configuration.Use(extensions);
        }
    }
}
