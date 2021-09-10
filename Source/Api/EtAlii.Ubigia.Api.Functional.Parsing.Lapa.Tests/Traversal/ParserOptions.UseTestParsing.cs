// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal static class ParserOptionsUseTestParsingExtension
    {
        public static FunctionalOptions UseTestParsing(this FunctionalOptions options)
        {
            return options.UseLapaParsing();
        }

        public static async Task<FunctionalOptions> UseTestParsing(this Task<FunctionalOptions> optionsTask)
        {
            var options = await optionsTask.ConfigureAwait(false);
            return options.UseLapaParsing();
        }
    }
}
