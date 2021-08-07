// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalContextOptionsUseLapaTraversalParserExtensions
    {
        /// <summary>
        /// Add Lapa GTL parsing to the options.
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TFunctionalOptions"></typeparam>
        /// <returns></returns>
        public static TFunctionalOptions UseLapaTraversalParser<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
            options.UseLapa();

            var editableOptions = (IEditableFunctionalOptions) options;
            editableOptions.ProcessorOptionsProvider = () => new TraversalProcessorOptions(options.ConfigurationRoot).UseLapa();

            return options;
        }
    }
}
