// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    public static class ParserOptionsUseAntlrExtension
    {
        public static FunctionalOptions UseAntlr(this FunctionalOptions options)
        {
            return options.Use(new[] {new AntrlParserExtension()});
        }
    }
}
