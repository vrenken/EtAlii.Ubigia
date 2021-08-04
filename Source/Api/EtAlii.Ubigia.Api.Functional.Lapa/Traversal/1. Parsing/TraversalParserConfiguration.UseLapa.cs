// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalParserOptionsUseLapaExtension
    {
        public static TraversalParserOptions UseLapa(this TraversalParserOptions options)
        {
            return options.Use(new[] {new LapaParserExtension()});
        }
    }
}
