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
        /// <typeparam name="TFunctionalOptions"></typeparam>
        /// <returns></returns>
        public static TFunctionalOptions UseAntlrTraversalParser<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
            options.UseAntlr();
            var editableOptions = (IEditableFunctionalOptions) options;
            editableOptions.ProcessorOptionsProvider = () => new TraversalProcessorOptions(options.ConfigurationRoot).UseAntlr();

            return options;
        }
    }
}
