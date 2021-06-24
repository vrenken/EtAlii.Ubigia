// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class SelectMultipleNodesAnnotation : NodeAnnotation
    {
        public SelectMultipleNodesAnnotation(PathSubject source) : base(source)
        {
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.Nodes}({Source?.ToString() ?? string.Empty})";
        }
    }
}
