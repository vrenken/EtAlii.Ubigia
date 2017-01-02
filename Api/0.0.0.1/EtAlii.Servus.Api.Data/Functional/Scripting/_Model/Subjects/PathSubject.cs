namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class PathSubject : Subject
    {
        public PathSubjectPart[] Parts { get; private set; }

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
