// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalProcessorOptionsUseLapaExtension
    {
        public static TraversalProcessorOptions UseLapa(this TraversalProcessorOptions options)
        {
            return options.Use(new IExtension[]
            {
                new LapaParserExtension(),
                new LapaProcessorExtension(options)
            });
        }
    }
}
