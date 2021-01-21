﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal static class TraversalParserConfigurationUseTestParserExtension
    {
        public static TraversalParserConfiguration UseTestParser(this TraversalParserConfiguration configuration)
        {
#if USE_LAPA_PARSER_IN_TESTS
                return configuration.UseLapa();
#else
            return configuration.UseAntlr();
#endif
        }
    }}
