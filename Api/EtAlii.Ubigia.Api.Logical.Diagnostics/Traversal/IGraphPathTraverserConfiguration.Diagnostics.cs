namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class GraphPathTraverserConfigurationDiagnosticsExtension
    {
        public static GraphPathTraverserConfiguration UseLogicalDiagnostics(this GraphPathTraverserConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IGraphPathTraverserExtension[]
            {
                new DiagnosticsGraphPathTraverserExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);
        }
    }
}