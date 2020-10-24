﻿namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class ClearAndSelectNodeValueAnnotation : NodeValueAnnotation 
    {
        public ClearAndSelectNodeValueAnnotation(PathSubject source) : base(source)
        {
        }
        
        public override string ToString()
        {
            return $"@{AnnotationPrefix.NodeValueClear}({Source?.ToString() ?? string.Empty})";
        }
    }
}