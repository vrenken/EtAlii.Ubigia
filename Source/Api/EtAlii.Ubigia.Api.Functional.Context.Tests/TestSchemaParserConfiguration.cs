// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class TestSchemaParserConfiguration : SchemaParserConfiguration
    {
        public TestSchemaParserConfiguration()
        {
#if USE_LAPA_PARSER_IN_TESTS
            Use(new LapaPathParserFactory());
#else
            Use(new AntlrPathParserFactory());
#endif

        }
    }
}
