namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;

    public abstract class PathSubject : Subject
    {
        public PathSubjectPart[] Parts { get; }

        protected PathSubject(PathSubjectPart part)
        {
            Parts = new [] { part };
        }

        protected PathSubject(PathSubjectPart[] parts)
        {
            Parts = parts;
        }

        public override string ToString()
        {
            return string.Concat(Parts.Select(part => part.ToString()));
        }

    }
}
