namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class VariablePathSubjectPart : PathSubjectPart
    {
        public string Name { get; private set; }

        public VariablePathSubjectPart(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return String.Format("${0}", Name);
        }
    }
}
