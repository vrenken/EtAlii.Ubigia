// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.CodeDom.Compiler;
    using Serilog;

    public class GraphContextExtensionWriter : IGraphContextExtensionWriter
    {
        public void Write(ILogger logger, IndentedTextWriter writer, Schema schema)
        {
            var structureFragment = schema.Structure;
            var schemaTextLines = schema.Text
                .Split('\n');
            var schemaLineCount = schemaTextLines.Length;

            var className = structureFragment.Name;

            logger
                .ForContext("StructureFragment", structureFragment, true)
                .Information("Writing schema processor extension for: {ClassName}", className);
            writer.WriteLine($"public static class GraphContext{className}Extension");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine("///<summary>");
            for (var i = 0; i < schemaLineCount; i++)
            {
                writer.WriteLine($"///{schemaTextLines[i]}");
            }

            writer.WriteLine("///</summary>");
            writer.WriteLine(structureFragment.Plurality == Plurality.Single
                ? $"public static Task<{className}> Process{className}(this IGraphContext context)"
                : $"public static IAsyncEnumerable<{className}> Process{className}(this IGraphContext context)");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine($"const string schemaText = @\"");
            writer.Indent += 1;

            for (var i = 0; i < schemaLineCount; i++)
            {
                var postfix = i == schemaLineCount - 1 ? "\";" : "";
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
