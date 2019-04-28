namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class ScriptParserConfiguration : IScriptParserConfiguration
    {
        public IScriptParserExtension[] Extensions { get; private set; }

        //private ILogicalContextConfiguration _parentConfiguration

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

//        public IScriptParserConfiguration Use(ILogicalContextConfiguration parentConfiguration)
//        [
//            _parentConfiguration = parentConfiguration ?? throw new ArgumentException(nameof(parentConfiguration))
//            return this
//        ]

    }
}