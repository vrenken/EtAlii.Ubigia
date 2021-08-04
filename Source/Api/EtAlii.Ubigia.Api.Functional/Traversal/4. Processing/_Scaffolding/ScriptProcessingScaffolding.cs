// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingScaffolding : IScaffolding
    {
        private readonly ITraversalProcessorOptions _options;

        public ScriptProcessingScaffolding(ITraversalProcessorOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            container.Register<IScriptProcessor, ScriptProcessor>();

            container.Register<IScriptProcessingContext, ScriptProcessingContext>();
            container.Register(() => _options.LogicalContext);
            container.Register(() => _options.ConfigurationRoot);
            container.Register(() => _options.ScriptScope);
            container.Register(() => _options);
        }
    }
}
