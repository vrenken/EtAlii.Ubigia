﻿namespace EtAlii.Ubigia.Api.Functional
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
