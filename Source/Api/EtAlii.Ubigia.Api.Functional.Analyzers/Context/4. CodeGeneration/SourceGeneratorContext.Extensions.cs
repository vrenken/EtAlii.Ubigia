// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Linq;
    using Microsoft.CodeAnalysis;

    internal static class SourceGeneratorContextExtensions
    {
        private const string SourceItemGroupMetadata = "build_metadata.AdditionalFiles.SourceItemGroup";

        public static string GetMSBuildProperty(
            this GeneratorExecutionContext context,
            string name,
            string defaultValue = "")
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue($"build_property.{name}", out var value);
            return value ?? defaultValue;
        }

        public static string[] GetMSBuildItems(this GeneratorExecutionContext context, string name)
        {
            return context
                .AdditionalFiles
                .Where(f => context.AnalyzerConfigOptions
                                .GetOptions(f)
                                .TryGetValue(SourceItemGroupMetadata, out var sourceItemGroup)
                            && sourceItemGroup == name)
                .Select(f => f.Path)
                .ToArray();
        }
    }
}
