// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.CodeDom.Compiler;
using Serilog;

public sealed class AnnotationCommentWriter : IAnnotationCommentWriter
{
    public void Write(ILogger logger, IndentedTextWriter writer, Annotation annotation)
    {
        if (annotation != null)
        {
            writer.WriteLine($"///<summary>");
            writer.WriteLine($"/// Graph traversal: {annotation}");
            writer.WriteLine($"///</summary>");
        }
    }
}
