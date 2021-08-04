// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class TraversalContextOptionsUseLapaTraversalParserExtensions
    {
        /// <summary>
        /// Add Antlr GTL parsing to the options.
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TTraversalContextOptions"></typeparam>
        /// <returns></returns>
        public static TTraversalContextOptions UseAntlrTraversalParser<TTraversalContextOptions>(this TTraversalContextOptions options)
            where TTraversalContextOptions : FunctionalContextOptions
        {
            var editableOptions = (IEditableFunctionalContextOptions) options;
            editableOptions.ParserOptions = new ParserOptions().UseAntlr();
            editableOptions.ProcessorOptionsProvider = () => new TraversalProcessorOptions().UseAntlr();

            return options;
        }
    }
}
