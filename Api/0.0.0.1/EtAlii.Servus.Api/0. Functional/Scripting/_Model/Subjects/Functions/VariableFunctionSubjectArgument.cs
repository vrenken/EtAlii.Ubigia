namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class VariableFunctionSubjectArgument : FunctionSubjectArgument
    {
        public string Name { get; private set; }

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
