﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaParserExtension : IScriptParserExtension, IScriptProcessorExtension
    {
        public void Initialize(Container container)
        {
            new LapaSequenceParsingScaffolding().Register(container);
            new LapaSubjectParsingScaffolding().Register(container);
            new LapaOperatorParsingScaffolding().Register(container);
            new LapaScriptParserScaffolding().Register(container);
            new LapaPathSubjectParsingScaffolding().Register(container);
            new LapaConstantParsingScaffolding().Register(container);
        }
    }
}
