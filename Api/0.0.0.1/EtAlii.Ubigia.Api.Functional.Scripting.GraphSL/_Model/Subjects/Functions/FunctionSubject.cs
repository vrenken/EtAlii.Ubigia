namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class FunctionSubject : Subject
    {
        public string Name { get; }

        public FunctionSubjectArgument[] Arguments { get; }

        public bool ShouldAcceptInput { get; set; }

        public FunctionSubject(string name)
        {
            Name = name;
            Arguments = new FunctionSubjectArgument[] {};
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
            return $"{Name}({String.Join(",", Arguments.Select(parameter => parameter.ToString()))})";
        }
    }
}
