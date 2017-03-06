namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class VariableFunctionSubjectArgument : FunctionSubjectArgument
    {
        public string Name { get; }

        public VariableFunctionSubjectArgument(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return String.Format("${0}", Name);
        }
    }
}
