// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal static class TraversalParserConfigurationUseTestParserExtension
    {
        public static TraversalParserConfiguration UseTestParser(this TraversalParserConfiguration configuration)
        {
                return configuration.UseLapa();
        }
    }
}
