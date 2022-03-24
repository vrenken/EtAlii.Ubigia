// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using Serilog;

    /// <inheritdoc />
    public sealed class GraphContextExtensionWriter : IGraphContextExtensionWriter
    {
        private readonly IVariableFinder _variableFinder;

        public GraphContextExtensionWriter(IVariableFinder variableFinder)
        {
            _variableFinder = variableFinder;
        }

        public void Write(ILogger logger, IndentedTextWriter writer, Schema schema)
        {
            var structureFragment = schema.Structure;
            var schemaTextLines = schema.Text
                .Split('\n');
            var schemaLineCount = schemaTextLines.Length;

            var className = structureFragment.Name;

            logger
                .ForContext("StructureFragment", structureFragment.Name)
                .Information("Writing schema processor extension for: {ClassName}", className);
            writer.WriteLine($"public static class GraphContext{className}Extension");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine("///<summary>");
            writer.WriteLine("///Query:");
            for (var i = 0; i < schemaLineCount; i++)
            {
                writer.WriteLine($"///{schemaTextLines[i]}");
            }

            writer.WriteLine("///</summary>");

            var variables = _variableFinder.FindVariables(schema);
            foreach (var variable in variables)
            {
                writer.WriteLine($"/// <param name=\"{variable}\" />");
            }

            var returnType = structureFragment.Plurality == Plurality.Single
                ? $"Task<{className}>"
                : $"IAsyncEnumerable<{className}>";

            var variablesAsString = string.Join(", ",variables.Select(v => $"string {v}"));
            var parameters = variables.Any()
                ? $"this IGraphContext context, {variablesAsString}"
                : $"this IGraphContext context";
            writer.WriteLine($"public static {returnType} Process{className}({parameters})");
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
            writer.WriteLine("var scope = new ExecutionScope();");
            foreach (var variable in variables)
            {
                writer.WriteLine($"scope.Variables.Add(\"{variable}\", new ScopeVariable({variable}, \"{variable}\"));");
            }

            writer.WriteLine();

            if (structureFragment.Plurality == Plurality.Single)
            {
                writer.WriteLine($"return context.ProcessSingle<{className}>(schemaText, resultMapper, scope);");
            }
            else
            {
                writer.WriteLine($"return context.ProcessMultiple<{className}>(schemaText, resultMapper, scope);");
            }
            writer.Indent -= 1;
            writer.WriteLine("}");

            writer.Indent -= 1;
            writer.WriteLine("}");
        }
    }
}
