// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
    internal class TestSchemaParserFactory : LapaSchemaParserFactory
    {
        public ISchemaParser Create() => base.Create(new TestSchemaParserConfiguration());
    }
}
