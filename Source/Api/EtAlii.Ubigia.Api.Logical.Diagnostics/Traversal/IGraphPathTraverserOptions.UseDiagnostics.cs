// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    public static class GraphPathTraverserOptionsDiagnosticsExtension
    {
        public static GraphPathTraverserOptions UseLogicalDiagnostics(this GraphPathTraverserOptions options)
        {
            var extensions = new IGraphPathTraverserExtension[]
            {
                new DiagnosticsGraphPathTraverserExtension(),
            };

            return options.Use(extensions);
        }
    }
}
