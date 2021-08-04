// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class ParserOptionsUseLapaExtension
    {
        public static ParserOptions UseLapa(this ParserOptions options)
        {
            return options.Use(new[] {new LapaParserExtension()});
        }
    }
}
