namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class ConstantFunctionSubjectArgument : FunctionSubjectArgument
    {
        public string Value { get; }

        public ConstantFunctionSubjectArgument(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"\"{Value}\"";
        }
    }
}
