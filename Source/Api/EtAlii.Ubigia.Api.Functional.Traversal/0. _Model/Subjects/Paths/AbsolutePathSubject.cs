namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class AbsolutePathSubject : NonRootedPathSubject
    {
        public AbsolutePathSubject(PathSubjectPart part)
        : base(part)
        {
        }

        public AbsolutePathSubject(PathSubjectPart[] parts)
        : base(parts)
        {
        }
    }
}
