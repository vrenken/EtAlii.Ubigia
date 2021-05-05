namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public abstract class NonRootedPathSubject : PathSubject
    {
        protected NonRootedPathSubject(PathSubjectPart part)
            : base(part)
        {
        }

        protected NonRootedPathSubject(PathSubjectPart[] parts)
            : base(parts)
        {
        }
    }
}
