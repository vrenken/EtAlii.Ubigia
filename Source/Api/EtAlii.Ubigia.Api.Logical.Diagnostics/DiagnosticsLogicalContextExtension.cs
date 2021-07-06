// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
                // Doesn't this pattern break with the general scaffolding principles?
                // More details can be found in the GitHub issue below:
                // https://github.com/vrenken/EtAlii.Ubigia/issues/88
                container.RegisterDecorator(typeof(ILogicalRootSet), typeof(LoggingLogicalRootSet));
            }
        }
    }
}
