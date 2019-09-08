namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

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
