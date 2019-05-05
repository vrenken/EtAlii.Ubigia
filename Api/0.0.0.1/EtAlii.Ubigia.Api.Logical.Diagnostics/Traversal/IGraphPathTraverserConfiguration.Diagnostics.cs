namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public static class IGraphPathTraverserConfigurationDiagnosticsExtension
    {
        public static GraphPathTraverserConfiguration UseLogicalDiagnostics(this GraphPathTraverserConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IGraphPathTraverserExtension[]
            {
                new DiagnosticsGraphPathTraverserExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            
            return configuration.Use(extensions);
        }
    }
}