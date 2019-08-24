namespace EtAlii.Ubigia.Api.Functional.Scripting
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
