// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.CodeDom.Compiler;
    using Serilog;

    public class NamespaceWriter : INamespaceWriter
    {
        private readonly IClassWriter _classWriter;
        private readonly ISchemaProcessorExtensionWriter _schemaProcessorExtensionWriter;

        public NamespaceWriter(IClassWriter classWriter, ISchemaProcessorExtensionWriter schemaProcessorExtensionWriter)
        {
            _classWriter = classWriter;
            _schemaProcessorExtensionWriter = schemaProcessorExtensionWriter;
        }

        public void Write(ILogger logger, IndentedTextWriter writer, Schema schema)
        {
            var @namespace = schema.Namespace ?? "EtAlii.Ubigia";
            logger.Information("Writing namespace: {Namespace}", @namespace);
            writer.WriteLine($"namespace {@namespace}");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine($"using System.Threading.Tasks;");
            if (schema.Structure.Plurality == Plurality.Multiple)
            {
                writer.WriteLine("using System.Collections.Generic;");
            }
            writer.WriteLine($"using {typeof(ISchemaProcessor).Namespace};");
            writer.WriteLine();

            _classWriter.Write(logger, writer, schema.Structure);

            writer.WriteLine();

            _schemaProcessorExtensionWriter.Write(logger, writer, schema);

            writer.Indent -= 1;
            writer.WriteLine("}");
        }
    }
}
