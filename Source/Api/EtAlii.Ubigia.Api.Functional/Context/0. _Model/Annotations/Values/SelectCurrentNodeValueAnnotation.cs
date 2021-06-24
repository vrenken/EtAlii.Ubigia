// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    public class SelectCurrentNodeValueAnnotation : ValueAnnotation
    {
        public SelectCurrentNodeValueAnnotation() : base(null)
        {
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.Node}()";
        }
    }
}
