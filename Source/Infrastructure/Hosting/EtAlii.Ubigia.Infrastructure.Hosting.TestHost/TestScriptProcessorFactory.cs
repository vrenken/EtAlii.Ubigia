﻿// This file seems not in the right place. Should be moved to somewhere better.
// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
#if USE_LAPA_PARSER_IN_TESTS
    internal class TestScriptProcessorFactory : LapaScriptProcessorFactory
#else
    internal class TestScriptProcessorFactory : AntlrScriptProcessorFactory
#endif
    {
    }
}