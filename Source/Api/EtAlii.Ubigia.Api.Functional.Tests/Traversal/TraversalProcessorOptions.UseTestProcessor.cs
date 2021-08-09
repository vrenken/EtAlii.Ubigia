﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr;

    internal static class TraversalProcessorOptionsUseTestProcessorExtension
    {
        public static FunctionalOptions UseTestProcessor(this FunctionalOptions options)
        {
#if USE_LAPA_PARSER_IN_TESTS
                return options.UseLapaParser();
#else
            return options.UseAntlrParser();
#endif
        }
    }}
