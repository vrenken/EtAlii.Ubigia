namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class StringConstantSubject : ConstantSubject
    {
        public readonly string Value;

        public StringConstantSubject(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return String.Format("\"{0}\"", Value);
        }
    }
}
