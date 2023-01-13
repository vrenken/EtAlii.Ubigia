// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional;

using EtAlii.Ubigia.Api.Functional.Context;
using EtAlii.xTechnology.MicroContainer;

public static class FunctionalOptionsUseLapaTraversalParsingExtensions
{
    /// <summary>
    /// Add Lapa GCL/GTL parsing to the options.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static FunctionalOptions UseLapaParsing(this FunctionalOptions options)
    {
        return options.Use(new IExtension[]
        {
            new LapaParserExtension(),
        });
    }
}
