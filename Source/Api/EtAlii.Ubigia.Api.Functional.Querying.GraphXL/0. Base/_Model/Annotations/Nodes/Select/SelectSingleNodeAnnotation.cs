namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class SelectSingleNodeAnnotation : NodeAnnotation
    {
        public SelectSingleNodeAnnotation(PathSubject source) : base(source)
        {
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.Node}({Source?.ToString() ?? string.Empty})";
        }
    }
}
