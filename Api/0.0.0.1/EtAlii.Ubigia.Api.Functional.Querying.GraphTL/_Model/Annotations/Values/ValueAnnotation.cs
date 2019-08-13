namespace EtAlii.Ubigia.Api.Functional
{
    public class ValueAnnotation : AnnotationNew
    {
        /// <summary>
        /// The target path subject, i.e. absolute, relative or rooted path towards one or multiple nodes.
        /// </summary>
        public PathSubject Target { get; }

        /// <summary>
        /// Create a new ValueAnnotation using the specified target.
        /// </summary>
        /// <param name="target"></param>
        public ValueAnnotation(PathSubject target)
        {
            Target = target;
        }
    }
}
