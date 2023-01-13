// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.CodeDom.Compiler;
using Serilog;

/// <inheritdoc />
public class NamespaceWriter : INamespaceWriter
{
    private readonly IClassWriter _classWriter;
    private readonly IGraphContextExtensionWriter _graphContextExtensionWriter;

    public NamespaceWriter(
        IClassWriter classWriter,
        IGraphContextExtensionWriter graphContextExtensionWriter)
    {
        _classWriter = classWriter;
        _graphContextExtensionWriter = graphContextExtensionWriter;
    }

    /// <inheritdoc />
    public void Write(ILogger logger, IndentedTextWriter writer, Schema schema)
    {
        var @namespace = schema.Namespace ?? "EtAlii.Ubigia";
        logger.Information("Writing namespace: {Namespace}", @namespace);
        writer.WriteLine($"namespace {@namespace}");
        writer.WriteLine("{");
        writer.Indent += 1;

        writer.WriteLine($"using System;");
        writer.WriteLine($"using System.Collections.Generic;");
        writer.WriteLine($"using System.Threading.Tasks;");
        writer.WriteLine($"using {typeof(ISchemaProcessor).Namespace};");
        writer.WriteLine($"using {typeof(ScopeVariable).Namespace};");
        writer.WriteLine();

        _classWriter.Write(logger, writer, schema.Structure);

        writer.WriteLine();

        _graphContextExtensionWriter.Write(logger, writer, schema);

        writer.Indent -= 1;
        writer.WriteLine("}");
    }
}
