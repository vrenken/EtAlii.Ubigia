// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal static class TraversalProcessorConfigurationUseTestProcessorExtension
    {
        public static TraversalProcessorConfiguration UseTestProcessor(this TraversalProcessorConfiguration configuration)
        {
#if USE_LAPA_PARSER_IN_TESTS
                return configuration.UseLapa();
#else
            return configuration.UseAntlr();
#endif
        }
    }}
