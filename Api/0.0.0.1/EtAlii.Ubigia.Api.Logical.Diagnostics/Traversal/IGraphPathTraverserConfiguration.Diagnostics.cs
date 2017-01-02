namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.xTechnology.Diagnostics;

    public static class IGraphPathTraverserConfigurationDiagnosticsExtension
    {
        public static IGraphPathTraverserConfiguration Use(this IGraphPathTraverserConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IGraphPathTraverserExtension[]
            {
                new DiagnosticsGraphPathTraverserExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}