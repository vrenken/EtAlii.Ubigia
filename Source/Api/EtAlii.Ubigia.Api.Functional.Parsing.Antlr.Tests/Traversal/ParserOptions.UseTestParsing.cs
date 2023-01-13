// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Antlr;

internal static class ParserOptionsUseTestParsingExtension
{
    public static FunctionalOptions UseTestParsing(this FunctionalOptions options)
    {
        return options.UseAntlrParsing();
    }

    public static async Task<FunctionalOptions> UseTestParsing(this Task<FunctionalOptions> optionsTask)
    {
        var options = await optionsTask.ConfigureAwait(false);
        return options.UseAntlrParsing();
    }
}
