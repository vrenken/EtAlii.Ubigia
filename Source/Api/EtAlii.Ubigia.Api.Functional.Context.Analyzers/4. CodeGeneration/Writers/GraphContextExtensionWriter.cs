// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.CodeDom.Compiler;
    using Serilog;

    public class GraphContextExtensionWriter : IGraphContextExtensionWriter
    {
        private readonly IStructureInstanceWriter _structureInstanceWriter;

        public GraphContextExtensionWriter(IStructureInstanceWriter structureInstanceWriter)
        {
            _structureInstanceWriter = structureInstanceWriter;
        }

        public void Write(ILogger logger, IndentedTextWriter writer, Schema schema)
        {
            var structureFragment = schema.Structure;
            var schemaText = schema.Text;
            var className = structureFragment.Name;

            logger
                .ForContext("StructureFragment", structureFragment, true)
                .Information("Writing schema processor extension for: {ClassName}", className);
            writer.WriteLine($"public static class GraphContext{className}Extension");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine(structureFragment.Plurality == Plurality.Single
                ? $"public static Task<{className}> Process{className}(this IGraphContext context)"
                : $"public static IAsyncEnumerable<{className}> Process{className}(this IGraphContext context)");
            writer.WriteLine("{");
            writer.Indent += 1;

            _structureInstanceWriter.Write(logger, writer, structureFragment, "rootStructure");

            var schemaTextLines = schemaText
                .Split('\n');
            writer.WriteLine($"const string schemaText = @\"");
            writer.Indent += 1;

            var length = schemaTextLines.Length;
            for (var i = 0; i < length; i++)
            {
                var postfix = i == length - 1 ? "\";" : "";
                writer.WriteLine($"{schemaTextLines[i]}{postfix}");
            }
            writer.Indent -= 1;

            writer.WriteLine($"var resultMapper = new {className}.ResultMapper();");

            if (structureFragment.Plurality == Plurality.Single)
            {
                writer.WriteLine($"return context.ProcessSingle<{className}>(schemaText, resultMapper);");
            }
            else
            {
                writer.WriteLine($"return context.ProcessMultiple<{className}>(schemaText, resultMapper);");
            }
            writer.Indent -= 1;
            writer.WriteLine("}");

            writer.Indent -= 1;
            writer.WriteLine("}");
        }
    }
}
