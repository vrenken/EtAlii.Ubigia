namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class RootPathSubjectPart : PathSubjectPart
    {
        private string Name { get; }

        public RootPathSubjectPart(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name + ":";
        }

    }
}
