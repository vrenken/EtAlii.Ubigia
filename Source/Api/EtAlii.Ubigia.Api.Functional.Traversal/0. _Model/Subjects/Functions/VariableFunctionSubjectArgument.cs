namespace EtAlii.Ubigia.Api.Functional.Traversal
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
