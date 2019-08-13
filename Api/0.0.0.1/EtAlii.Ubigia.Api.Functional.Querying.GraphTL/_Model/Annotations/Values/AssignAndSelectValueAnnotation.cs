namespace EtAlii.Ubigia.Api.Functional
{
    public class AssignAndSelectValueAnnotation : ValueAnnotation
    {
        public Subject Subject { get; }
        
        public AssignAndSelectValueAnnotation(PathSubject target, Subject subject) : base(target)
        {
            Subject = subject;
        }
    }
}
