namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    public class PathSubject : Subject
    {
        public PathSubjectPart[] Parts { get; private set; }
        public bool IsAbsolute { get { return Parts.FirstOrDefault() is IsParentOfPathSubjectPart; } }

        public PathSubject(PathSubjectPart part)
        {
            this.Parts = new PathSubjectPart[] { part };
        }

        public PathSubject(PathSubjectPart[] parts)
        {
            this.Parts = parts;
        }

        public override string ToString()
        {
            return String.Concat(Parts.Select(part => part.ToString()));
        }

    }
}
