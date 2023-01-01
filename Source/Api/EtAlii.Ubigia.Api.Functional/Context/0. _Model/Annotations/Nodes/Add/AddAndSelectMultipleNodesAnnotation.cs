// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class AddAndSelectMultipleNodesAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The identity of the nodes to be added (this can either be a name or a variable).
        /// </summary>
        public NodeIdentity Identity { get; }

        public AddAndSelectMultipleNodesAnnotation(PathSubject source, NodeIdentity identity) : base(source)
        {
            Identity = identity;
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.NodesAdd}({Source?.ToString() ?? string.Empty}, {Identity})";
        }
    }
}
