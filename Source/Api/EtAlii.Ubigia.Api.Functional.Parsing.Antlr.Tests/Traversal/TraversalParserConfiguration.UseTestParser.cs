// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

    internal static class TraversalParserConfigurationUseTestParserExtension
    {
        public static TraversalParserConfiguration UseTestParser(this TraversalParserConfiguration configuration)
        {
                return configuration.UseAntlr();
        }
    }
}
