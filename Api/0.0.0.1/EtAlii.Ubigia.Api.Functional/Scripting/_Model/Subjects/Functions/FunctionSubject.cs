namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class FunctionSubject : Subject
    {
        public string Name { get; private set; }

        public FunctionSubjectArgument[] Arguments { get; private set; }

        public bool ShouldAcceptInput { get; set; }

        public FunctionSubject(string name)
        {
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
            return String.Format("{0}({1})",Name, String.Join(",", Arguments.Select(parameter => parameter.ToString())));
        }
    }
}
