// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    internal interface ISchemaParserFactory
    {
        ISchemaParser Create(SchemaParserConfiguration configuration);
    }
}
