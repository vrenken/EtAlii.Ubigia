// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.CodeDom.Compiler;
    using Serilog;

    public class SchemaProcessorExtensionWriter : ISchemaProcessorExtensionWriter
    {
        public void Write(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment)
        {
            var className = structureFragment.Name;
            logger
                .ForContext("StructureFragment", structureFragment, true)
                .Information("Writing schema processor extension for: {ClassName}", className);
            writer.WriteLine($"public static class SchemaProcessor{className}Extension");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine($"public static Task<SchemaProcessingResult<{className}>> Process{className}(this ISchemaProcessor processor)");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine("return null;");
            writer.Indent -= 1;
            writer.WriteLine("}");

            writer.Indent -= 1;
            writer.WriteLine("}");

        }
    }
}
