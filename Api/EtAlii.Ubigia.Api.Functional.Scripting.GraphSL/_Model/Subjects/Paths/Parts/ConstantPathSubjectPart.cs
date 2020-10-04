namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public class ConstantPathSubjectPart : PathSubjectPart
    {
        public string Name { get; }

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
