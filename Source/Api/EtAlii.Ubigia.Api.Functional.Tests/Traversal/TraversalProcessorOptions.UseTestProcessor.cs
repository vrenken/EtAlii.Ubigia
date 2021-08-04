// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

    internal static class TraversalProcessorOptionsUseTestProcessorExtension
    {
        public static TraversalProcessorOptions UseTestProcessor(this TraversalProcessorOptions options)
        {
#if USE_LAPA_PARSER_IN_TESTS
                return options.UseLapa();
#else
            return options.UseAntlr();
#endif
        }
    }}
