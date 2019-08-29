﻿namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class LinkAndSelectSingleNodeAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The target path subject, i.e. absolute, relative or rooted path towards one or multiple nodes.
        /// </summary>
        public PathSubject Target { get; }

        /// <summary>
        /// The relative target path subject where the source should be linked to.
        /// </summary>
        public NonRootedPathSubject TargetLink { get; }

        public LinkAndSelectSingleNodeAnnotation(PathSubject source, PathSubject target, NonRootedPathSubject targetLink) : base(source)
        {
            Target = target;
            TargetLink = targetLink;
        }
    }
}
