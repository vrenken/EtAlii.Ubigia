namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class AssignAndSelectValueAnnotation : ValueAnnotation
    {
        public Subject Subject { get; }

        public AssignAndSelectValueAnnotation(PathSubject source, Subject subject) : base(source)
        {
            Subject = subject;
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.ValueSet}({Source?.ToString() ?? string.Empty}, {Subject?.ToString() ?? string.Empty})";
        }
    }
}
