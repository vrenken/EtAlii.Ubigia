// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    internal sealed class LapaParserExtension : IExtension
    {
        public void Initialize(IRegisterOnlyContainer container)
        {
            new LapaSchemaParserScaffolding().Register(container);
            new LapaScriptParserScaffolding().Register(container);

            new LapaSequenceParsingScaffolding().Register(container);
            new LapaSubjectParsingScaffolding().Register(container);
            new LapaOperatorParsingScaffolding().Register(container);
            new LapaPathSubjectParsingScaffolding().Register(container);
            new LapaConstantParsingScaffolding().Register(container);
        }
    }
}
