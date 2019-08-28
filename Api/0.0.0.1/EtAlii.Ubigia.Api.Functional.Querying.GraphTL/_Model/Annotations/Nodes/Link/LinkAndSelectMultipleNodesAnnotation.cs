namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class LinkAndSelectMultipleNodesAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The target path subject, i.e. absolute, relative or rooted path towards one or multiple nodes.
        /// </summary>
        public PathSubject Target { get; }

        /// <summary>
        /// The relative target path subject where the source should be linked to.
        /// </summary>
        public PathSubject TargetLink { get; }

        public LinkAndSelectMultipleNodesAnnotation(PathSubject source, PathSubject target, RelativePathSubject targetLink) : base(source)
        {
            Target = target;
            TargetLink = targetLink;
        }
    }
}
