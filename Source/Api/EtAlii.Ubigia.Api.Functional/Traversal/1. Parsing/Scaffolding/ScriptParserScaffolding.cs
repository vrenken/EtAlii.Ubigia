// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ScriptParserScaffolding : IScaffolding
    {
        private readonly ParserOptions _options;

        public ScriptParserScaffolding(ParserOptions options)
        {
            _options = options;
        }

        public void Register(Container container)
        {
            container.Register(() => _options.ConfigurationRoot);
        }
    }
}
