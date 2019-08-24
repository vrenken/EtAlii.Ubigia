namespace EtAlii.Ubigia.Api.Functional.Scripting
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
