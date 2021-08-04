// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

    internal static class TraversalParserOptionsUseTestParserExtension
    {
        public static TraversalParserOptions UseTestParser(this TraversalParserOptions options)
        {
                return options.UseAntlr();
        }
    }
}
