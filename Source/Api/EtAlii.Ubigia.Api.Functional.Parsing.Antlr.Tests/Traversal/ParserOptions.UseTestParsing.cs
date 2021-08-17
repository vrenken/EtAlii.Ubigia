// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr;

    internal static class ParserOptionsUseTestParsingExtension
    {
        public static FunctionalOptions UseTestParsing(this FunctionalOptions options)
        {
                return options.UseAntlrParsing();
        }
    }
}
