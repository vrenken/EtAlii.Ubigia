// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class TraversalParserConfigurationUseAntlrExtension
    {
        public static TraversalParserConfiguration UseAntlr(this TraversalParserConfiguration configuration)
        {
            return configuration.Use(new[] {new AntrlParserExtension()});
        }
    }
}
