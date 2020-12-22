namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class VariablePathSubjectPart : PathSubjectPart
    {
        public string Name { get; }

        public VariablePathSubjectPart(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"${Name}";
        }
    }
}
