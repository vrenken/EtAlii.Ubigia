namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;

    public class RootedPathSubject : PathSubject
    {
        public string Root { get; private set; }

        public RootedPathSubject(string root, PathSubjectPart part) 
            : base(part)
        {
            Root = root;
        }

        public RootedPathSubject(string root, PathSubjectPart[] parts)
            : base(parts)
        {
            Root = root;
        }

        public override string ToString()
        {
            return Root + ":" + String.Concat(Parts.Select(part => part.ToString()));
        }

    }
}
