// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using EtAlii.Ubigia.Api.Functional.Traversal;

public class SelectSingleNodeAnnotation : NodeAnnotation
{
    public SelectSingleNodeAnnotation(PathSubject source) : base(source)
    {
    }

    public override string ToString()
    {
        return $"@{AnnotationPrefix.Node}({Source})";
    }
}
