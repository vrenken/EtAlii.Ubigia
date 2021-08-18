// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.xTechnology.MicroContainer;

    public static class FunctionalOptionsUseLapaTraversalParsingExtensions
    {
        /// <summary>
        /// Add Lapa GCL/GTL parsing to the options.
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TFunctionalOptions"></typeparam>
        /// <returns></returns>
        public static TFunctionalOptions UseLapaParsing<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
            return options.Use(new IExtension[]
            {
                new LapaParserExtension(),
            });
        }
    }
}
