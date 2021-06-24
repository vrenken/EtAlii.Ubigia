// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    /// <summary>
    /// The Query class contains all information needed to execute graph traversal actions on the current infrastructureClient.
    /// </summary>
#pragma warning disable CA1724// This class really has a purpose.
    public class Schema
#pragma warning restore CA1724
    {
        public string Namespace { get; }
        public string ContextName { get; }

        public StructureFragment Structure { get; }

        public string Text { get; }

        public Schema(StructureFragment structure, string @namespace, string contextName, string text)
        {
            Structure = structure;
            Namespace = @namespace;
            ContextName = contextName;
            Text = text;
        }
    }
}
