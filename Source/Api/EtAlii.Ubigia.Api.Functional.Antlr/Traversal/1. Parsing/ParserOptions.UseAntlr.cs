// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    public static class ParserOptionsUseAntlrExtension
    {
        public static ParserOptions UseAntlr(this ParserOptions options)
        {
            return options.Use(new[] {new AntrlParserExtension()});
        }
    }
}
