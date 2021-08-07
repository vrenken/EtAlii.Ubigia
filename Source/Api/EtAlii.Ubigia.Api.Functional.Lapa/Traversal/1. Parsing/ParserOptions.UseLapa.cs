// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class ParserOptionsUseLapaExtension
    {
        public static FunctionalOptions UseLapa(this FunctionalOptions options)
        {
            return options.Use(new[] {new LapaParserExtension()});
        }
    }
}
