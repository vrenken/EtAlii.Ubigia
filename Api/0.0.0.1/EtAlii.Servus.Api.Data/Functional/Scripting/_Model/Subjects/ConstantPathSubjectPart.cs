namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public class ConstantPathSubjectPart : PathSubjectPart
    {
        public string Name { get; private set; }

        public ConstantPathSubjectPart(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
