// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

    internal static class ParserOptionsUseTestParserExtension
    {
        public static ParserOptions UseTestParser(this ParserOptions options)
        {
                return options.UseAntlr();
        }
    }
}
