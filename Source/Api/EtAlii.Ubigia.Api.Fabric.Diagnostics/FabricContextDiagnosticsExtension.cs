// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class FabricContextDiagnosticsExtension : IFabricContextExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal FabricContextDiagnosticsExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);

            if (_diagnostics.EnableLogging)
            {
                container.RegisterDecorator(typeof(IEntryContext), typeof(LoggingEntryContext));
            }
        }
    }
}
