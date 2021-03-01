// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.CodeDom.Compiler;
    using Serilog;

    public class SchemaProcessorExtensionWriter : ISchemaProcessorExtensionWriter
    {
        private readonly IStructureInstanceWriter _structureInstanceWriter;

        public SchemaProcessorExtensionWriter(IStructureInstanceWriter structureInstanceWriter)
        {
            _structureInstanceWriter = structureInstanceWriter;
        }

        public void Write(ILogger logger, IndentedTextWriter writer, Schema schema)
        {
            var structureFragment = schema.Structure;
            var @namespace = schema.Namespace;
            var contextName = schema.ContextName;
            var className = structureFragment.Name;

            logger
                .ForContext("StructureFragment", structureFragment, true)
                .Information("Writing schema processor extension for: {ClassName}", className);
            writer.WriteLine($"public static class SchemaProcessor{className}Extension");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine(structureFragment.Plurality == Plurality.Single
                ? $"public static Task<{className}> Process{className}(this ISchemaProcessor processor)"
                : $"public static IAsyncEnumerable<{className}> Process{className}(this ISchemaProcessor processor)");
            writer.WriteLine("{");
            writer.Indent += 1;

            _structureInstanceWriter.Write(logger, writer, structureFragment, "rootStructure");

            writer.WriteLine($"var schema = new Schema(rootStructure, \"{@namespace}\", \"{contextName}\");");


            if (structureFragment.Plurality == Plurality.Single)
            {
                writer.WriteLine($"return processor.ProcessSingle<{className}>(schema);");
            }
            else
            {
                writer.WriteLine($"return processor.ProcessMultiple<{className}>(schema);");
            }
            writer.Indent -= 1;
            writer.WriteLine("}");

            writer.Indent -= 1;
            writer.WriteLine("}");
        }
    }
}
