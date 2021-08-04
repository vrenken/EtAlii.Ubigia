// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal static class TraversalOptionsConfigurationUseTestParserExtension
    {
        public static TraversalParserOptions UseTestParser(this TraversalParserOptions options)
        {
                return options.UseLapa();
        }
    }
}
