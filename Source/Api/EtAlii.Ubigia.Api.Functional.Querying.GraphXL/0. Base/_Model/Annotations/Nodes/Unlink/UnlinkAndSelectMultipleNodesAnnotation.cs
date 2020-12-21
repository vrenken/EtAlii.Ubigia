namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class UnlinkAndSelectMultipleNodesAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The target path subject, i.e. absolute, relative or rooted path towards one or multiple nodes.
        /// </summary>
        public PathSubject Target { get; }

        /// <summary>
        /// The relative target path subject where the source should be unlinked from.
        /// </summary>
        public NonRootedPathSubject TargetLink { get; }

        public UnlinkAndSelectMultipleNodesAnnotation(PathSubject source, PathSubject target, NonRootedPathSubject targetLink) : base(source)
        {
            Target = target;
            TargetLink = targetLink;
        }
        
        public override string ToString()
        {
            return $"@{AnnotationPrefix.NodesUnlink}({Source?.ToString() ?? string.Empty}, {Target?.ToString() ?? string.Empty}, {TargetLink?.ToString() ?? string.Empty})";
        }
    }
}
