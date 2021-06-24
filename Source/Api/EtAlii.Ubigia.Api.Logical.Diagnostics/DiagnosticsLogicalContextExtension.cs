// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.MicroContainer;

    public class DiagnosticsLogicalContextExtension : ILogicalContextExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal DiagnosticsLogicalContextExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _diagnostics);

            if (_diagnostics.EnableLogging)
            {
                // TODO: This is wrong and breaks with the scaffolding pattern.
                container.RegisterDecorator(typeof(ILogicalRootSet), typeof(LoggingLogicalRootSet));
            }
        }
    }
}
