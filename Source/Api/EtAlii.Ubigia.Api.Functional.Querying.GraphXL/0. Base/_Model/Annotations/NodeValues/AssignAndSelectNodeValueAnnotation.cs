namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class AssignAndSelectNodeValueAnnotation : NodeValueAnnotation
    {
        public Subject Subject { get; }

        public AssignAndSelectNodeValueAnnotation(PathSubject source, Subject subject) : base(source)
        {
            Subject = subject;
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.NodeValueSet}({Source?.ToString() ?? string.Empty}, {Subject?.ToString() ?? string.Empty})";
        }
    }
}
