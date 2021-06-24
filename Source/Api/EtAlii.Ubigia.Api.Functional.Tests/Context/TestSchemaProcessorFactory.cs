// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Context;

#if USE_LAPA_PARSER_IN_TESTS
    internal class TestSchemaProcessorFactory : LapaSchemaProcessorFactory
#else
    internal class TestSchemaProcessorFactory : AntlrSchemaProcessorFactory
#endif
    {
    }
}
