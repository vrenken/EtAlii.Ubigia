namespace EtAlii.Servus.Api.Functional
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
