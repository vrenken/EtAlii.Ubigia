// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    public class SelectCurrentNodeAnnotation : NodeAnnotation
    {
        public SelectCurrentNodeAnnotation() : base(null)
        {
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.Node}()";
        }
    }
}
