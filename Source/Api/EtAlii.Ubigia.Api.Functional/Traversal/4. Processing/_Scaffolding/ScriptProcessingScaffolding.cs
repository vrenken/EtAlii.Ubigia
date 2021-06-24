// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptProcessingScaffolding : IScaffolding
    {
        private readonly ITraversalProcessorConfiguration _configuration;

        public ScriptProcessingScaffolding(ITraversalProcessorConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<IScriptProcessor, ScriptProcessor>();

            container.Register<IScriptProcessingContext, ScriptProcessingContext>();
            container.Register(() => _configuration.LogicalContext);
            container.Register(() => _configuration.ScriptScope);
            container.Register(() => _configuration);
        }
    }
}
