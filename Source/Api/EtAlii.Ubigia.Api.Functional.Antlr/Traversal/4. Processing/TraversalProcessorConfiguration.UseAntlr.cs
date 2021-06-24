// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class TraversalProcessorConfigurationUseAntlrExtension
    {
        public static TraversalProcessorConfiguration UseAntlr(this TraversalProcessorConfiguration configuration)
        {
            return configuration.Use(new IExtension[]
            {
                new AntrlParserExtension(),
                new AntlrProcessorExtension(configuration)
            });
        }
    }
}
