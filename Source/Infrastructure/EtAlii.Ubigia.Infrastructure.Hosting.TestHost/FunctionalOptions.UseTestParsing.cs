// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Antlr;

/// <summary>
/// Add the configured test GTL parsing to the options.
/// </summary>
public static class FunctionalOptionsUseTestParsingExtension
{
    // TODO: is this file in the right project?

    /// <summary>
    /// Use the text parser configured for testing.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static FunctionalOptions UseTestParsing(this FunctionalOptions options)
    {
#if USE_LAPA_PARSING_IN_TESTS
            return options.UseLapaParsing();
#else
        return options.UseAntlrParsing();
#endif
    }

    /// <summary>
    /// Use the text parser configured for testing.
    /// </summary>
    /// <param name="optionsTask"></param>
    /// <returns></returns>
    public static async Task<FunctionalOptions> UseTestParsing(this Task<FunctionalOptions> optionsTask)
    {
        var options = await optionsTask.ConfigureAwait(false);
#if USE_LAPA_PARSING_IN_TESTS
            return options.UseLapaParsing();
#else
        return options.UseAntlrParsing();
#endif
    }
}
