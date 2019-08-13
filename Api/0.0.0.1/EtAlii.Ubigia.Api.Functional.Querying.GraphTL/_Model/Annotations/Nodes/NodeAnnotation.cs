namespace EtAlii.Ubigia.Api.Functional
{
    public abstract class NodeAnnotation : AnnotationNew
    {
        /// <summary>
        /// The target path subject, i.e. absolute, relative or rooted path towards one or multiple nodes.
        /// </summary>
        public PathSubject Target { get; }

        /// <summary>
        /// Create a new NodeAnnotation inheriting instance given the specified target.
        /// </summary>
        /// <param name="target"></param>
        protected NodeAnnotation(PathSubject target)
        {
            Target = target;
        }
    }
}
