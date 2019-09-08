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

        public override string ToString()
        {
            return $"@{AnnotationPrefix.ValueAssign}({Source?.ToString() ?? string.Empty}, {Subject?.ToString() ?? string.Empty})";
        }
    }
}
