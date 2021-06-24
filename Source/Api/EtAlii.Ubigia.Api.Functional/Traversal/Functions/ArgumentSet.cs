// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using System.Reflection;

    public class ArgumentSet
    {
        public object[] Arguments { get; }

        public TypeInfo[] ArgumentTypeInfos { get; }

        public ArgumentSet(params object[] arguments)
        {
            Arguments = arguments;

            ArgumentTypeInfos = arguments
                .Select(a => a?.GetType().GetTypeInfo())
                .ToArray();
        }

        public override string ToString()
        {
            return string.Join(", ", Arguments.Select(a => a != null ? a.GetType().Name : "NULL"));
        }
    }
}
