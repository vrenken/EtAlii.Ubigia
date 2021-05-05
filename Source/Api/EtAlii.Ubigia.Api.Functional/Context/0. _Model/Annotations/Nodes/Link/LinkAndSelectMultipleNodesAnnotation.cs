namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class LinkAndSelectMultipleNodesAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The target path subject, i.e. absolute, relative or rooted path towards one or multiple nodes.
        /// </summary>
        public PathSubject Target { get; }

        /// <summary>
        /// The relative target path subject where the source should be linked to.
        /// </summary>
        public NonRootedPathSubject TargetLink { get; }

        public LinkAndSelectMultipleNodesAnnotation(PathSubject source, PathSubject target, NonRootedPathSubject targetLink) : base(source)
        {
            Target = target;
            TargetLink = targetLink;
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.NodesLink}({Source?.ToString() ?? string.Empty}, {Target?.ToString() ?? string.Empty}, {TargetLink?.ToString() ?? string.Empty})";
        }
    }
}
