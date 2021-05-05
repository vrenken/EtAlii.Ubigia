namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class ClearAndSelectValueAnnotation : ValueAnnotation
    {
        public ClearAndSelectValueAnnotation(PathSubject source) : base(source)
        {
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.ValueClear}({Source?.ToString() ?? string.Empty})";
        }
    }
}
