namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class AssignAndSelectValueAnnotation : ValueAnnotation 
    {
        public Subject Subject { get; }
        
        public AssignAndSelectValueAnnotation(PathSubject source, Subject subject) : base(source)
        {
            Subject = subject;
        }
    }
}
