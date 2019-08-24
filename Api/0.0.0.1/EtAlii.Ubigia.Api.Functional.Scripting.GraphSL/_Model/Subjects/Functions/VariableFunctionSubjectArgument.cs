namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public class VariableFunctionSubjectArgument : FunctionSubjectArgument
    {
        public string Name { get; }

        public VariableFunctionSubjectArgument(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"${Name}";
        }
    }
}
