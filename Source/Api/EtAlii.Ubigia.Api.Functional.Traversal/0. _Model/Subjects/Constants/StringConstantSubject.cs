namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class StringConstantSubject : ConstantSubject
    {
        public readonly string Value;

        public StringConstantSubject(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"\"{Value}\"";
        }
    }
}
