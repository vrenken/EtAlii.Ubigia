namespace EtAlii.Ubigia.Api.Functional.Context
{
    public class SelectCurrentNodeAnnotation : NodeAnnotation
    {
        public SelectCurrentNodeAnnotation() : base(null)
        {
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.Node}()";
        }
    }
}
