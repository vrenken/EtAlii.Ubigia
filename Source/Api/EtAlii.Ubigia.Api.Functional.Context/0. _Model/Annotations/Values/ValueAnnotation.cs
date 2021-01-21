namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class ValueAnnotation : Annotation
    {
        /// <summary>
        /// The source path subject, i.e. absolute, relative or rooted path towards one or multiple nodes.
        /// </summary>
        public PathSubject Source { get; }

        /// <summary>
        /// Create a new ValueAnnotation using the specified source.
        /// </summary>
        /// <param name="source"></param>
        public ValueAnnotation(PathSubject source)
        {
            Source = source;
        }
    }
}
