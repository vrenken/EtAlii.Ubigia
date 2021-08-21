// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    public static class GraphContextOptionsUseAntlrParsingExtension
    {
        /// <summary>
        /// Add Antlr GCL/GTL parsing to the options.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static FunctionalOptions UseAntlrParsing(this FunctionalOptions options)
        {
            options.Use(new IExtension[]
            {
                new AntrlParserExtension()
            });

            return options;
        }
    }
}
