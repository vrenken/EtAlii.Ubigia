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

            var variables = _variableFinder.FindVariables(schema);
            var returnType = structureFragment.Plurality == Plurality.Single
                ? $"Task<{className}>"
                : $"IAsyncEnumerable<{className}>";

            var variablesAsParameterString = string.Join(", ",variables.Select(v => $"string {v}"));
            var parameters = variables.Any()
                ? $"this IGraphContext context, {variablesAsParameterString}"
                : $"this IGraphContext context";

            var variablesAsArgumentString = string.Join(", ",variables.Select(v => $"{v}"));
            var arguments = variables.Any()
                ? $"context, {variablesAsArgumentString}, scope"
                : $"context, scope";

            // Write override method (with internal scope variable).
            WriteSummary(writer, schemaLineCount, schemaTextLines);
            WriteVariableParametersComment(writer, variables);
            writer.WriteLine($"public static {returnType} Process{className}({parameters})");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine("var scope = new ExecutionScope();");
            writer.WriteLine($"return Process{className}({arguments});");

            writer.Indent -= 1;
            writer.WriteLine("}");
            writer.WriteLine();

            // Write core method (with scope parameter).
            WriteSummary(writer, schemaLineCount, schemaTextLines);
            WriteVariableParametersComment(writer, variables);
            writer.WriteLine($"/// <param name=\"scope\" />");


            variablesAsParameterString = string.Join(", ",variables.Select(v => $"string {v}"));
            parameters = variables.Any()
                ? $"this IGraphContext context, {variablesAsParameterString}, ExecutionScope scope"
                : $"this IGraphContext context, ExecutionScope scope";

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

        private static void WriteVariableParametersComment(IndentedTextWriter writer, string[] variables)
        {
            foreach (var variable in variables)
            {
                writer.WriteLine($"/// <param name=\"{variable}\" />");
            }
        }

        private static void WriteSummary(IndentedTextWriter writer, int schemaLineCount, string[] schemaTextLines)
        {
            writer.WriteLine("///<summary>");
            writer.WriteLine("///Query:");
            for (var i = 0; i < schemaLineCount; i++)
            {
                writer.WriteLine($"///{schemaTextLines[i]}");
            }

            writer.WriteLine("///</summary>");
        }
    }
}
