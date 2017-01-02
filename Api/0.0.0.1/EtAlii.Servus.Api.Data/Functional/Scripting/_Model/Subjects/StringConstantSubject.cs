namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;

    public class StringConstantSubject : ConstantSubject
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
