namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class SelectValueAnnotation : ValueAnnotation
    {
        public SelectValueAnnotation(PathSubject source) : base(source)
        {
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.Value}({Source?.ToString() ?? string.Empty})";
        }
    }
}
