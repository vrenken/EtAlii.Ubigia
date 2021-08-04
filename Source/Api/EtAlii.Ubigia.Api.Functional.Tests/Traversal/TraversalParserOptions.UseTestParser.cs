// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

    internal static class TraversalParserOptionsUseTestParserExtension
    {
        public static TraversalParserOptions UseTestParser(this TraversalParserOptions options)
        {
#if USE_LAPA_PARSER_IN_TESTS
                return options.UseLapa();
#else
            return options.UseAntlr();
#endif
        }
    }
}
