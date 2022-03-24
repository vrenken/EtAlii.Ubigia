// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    public sealed class FunctionSubject : Subject
    {
        public string Name { get; }

        public FunctionSubjectArgument[] Arguments { get; }

        public bool ShouldAcceptInput { get; set; }

        public FunctionSubject(string name)
        {
            Name = name;
            Arguments = Array.Empty<FunctionSubjectArgument>();
        }

        public FunctionSubject(string name, FunctionSubjectArgument argument)
        {
            Name = name;
            Arguments = new[] { argument };
        }

        public FunctionSubject(string name, params FunctionSubjectArgument[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public override string ToString()
        {
            return $"{Name}({string.Join(",", Arguments.Select(parameter => parameter.ToString()))})";
        }
    }
}
