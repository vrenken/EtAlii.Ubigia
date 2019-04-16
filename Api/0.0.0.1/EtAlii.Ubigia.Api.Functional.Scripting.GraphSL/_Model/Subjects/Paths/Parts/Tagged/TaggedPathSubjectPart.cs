namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class TaggedPathSubjectPart : PathSubjectPart
    {
        public string Name { get; }
        public string Tag { get; }

        public TaggedPathSubjectPart(string name, string tag)
        {
            Name = name ?? String.Empty;
            Tag = tag ?? String.Empty;
        }

        public override string ToString()
        {
            return $"{Name}#{Tag}";
        }
    }
}
