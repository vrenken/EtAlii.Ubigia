// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Threading;
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingLoggingScaffolding : IScaffolding
    {
        private readonly DiagnosticsConfigurationSection _configuration;

        public ScriptProcessingLoggingScaffolding(DiagnosticsConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            if (_configuration.InjectLogging) // logging is enabled.
            {
                container.RegisterDecorator(typeof(IScriptProcessor), typeof(LoggingScriptProcessor));

                container.RegisterDecorator(typeof(IAbsolutePathSubjectProcessor), typeof(LoggingAbsolutePathSubjectProcessor));
                container.RegisterDecorator(typeof(IRelativePathSubjectProcessor), typeof(LoggingRelativePathSubjectProcessor));
                container.RegisterDecorator(typeof(IRootedPathSubjectProcessor), typeof(LoggingRootedPathSubjectProcessor));

                container.RegisterDecorator(typeof(IRootPathProcessor), typeof(LoggingRootPathProcessor));

                container.Register<IContextCorrelator, ContextCorrelator>();
            }
        }
    }
}
