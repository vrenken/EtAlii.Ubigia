// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class TraversalProcessorOptionsUseAntlrExtension
    {
        public static TraversalProcessorOptions UseAntlr(this TraversalProcessorOptions options)
        {
            return options.Use(new IExtension[]
            {
                new AntrlParserExtension(),
                new AntlrProcessorExtension(options)
            });
        }
    }
}
