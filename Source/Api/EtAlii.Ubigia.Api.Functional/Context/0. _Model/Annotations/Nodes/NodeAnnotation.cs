// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public abstract class NodeAnnotation : Annotation
    {
        /// <summary>
        /// The source path subject, i.e. absolute, relative or rooted path towards one or multiple nodes.
        /// </summary>
        public PathSubject Source { get; }

        /// <summary>
        /// Create a new NodeAnnotation inheriting instance given the specified target.
        /// </summary>
        /// <param name="source"></param>
        protected NodeAnnotation(PathSubject source)
        {
            Source = source;
        }
    }
}
