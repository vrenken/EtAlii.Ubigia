namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;

    public class VariableSubject : Subject
    {
        public readonly string Name;

        public VariableSubject(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return String.Format("${0}", Name);
        }
    }
}
