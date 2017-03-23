﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Logical;

    internal class ScriptParserConfiguration : IScriptParserConfiguration
    {
        public IScriptParserExtension[] Extensions { get; private set; }

        private ILogicalContextConfiguration _parentConfiguration;

        public ScriptParserConfiguration()
        {
            Extensions = new IScriptParserExtension[0];
        }

        public IScriptParserConfiguration Use(IScriptParserExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IScriptParserConfiguration Use(ILogicalContextConfiguration parentConfiguration)
        {
            if (parentConfiguration == null)
            {
                throw new ArgumentException(nameof(parentConfiguration));
            }
            _parentConfiguration = parentConfiguration;
            return this;
        }

    }
}