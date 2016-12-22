namespace EtAlii.Servus.Api.Functional
{
    public class RootPathSubjectPart : PathSubjectPart
    {
        public string Name { get; private set; }

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
