// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.CodeDom.Compiler;
    using Serilog;

    public class StructureInstanceWriter : IStructureInstanceWriter
    {
        public void Write(ILogger logger, IndentedTextWriter writer, StructureFragment fragment, string variableName)
        {
            var fragmentName = ToCamelCase(fragment.Name);
            writer.WriteLine($"{nameof(StructureFragment)} Create{fragment.Name}{nameof(StructureFragment)}()");
            writer.WriteLine("{");
            writer.Indent += 1;
            writer.WriteLine($"var {fragmentName}Name = \"{fragment.Name}\";");
            writer.WriteLine($"var {fragmentName}Plurality = Plurality.{fragment.Plurality};");
            writer.WriteLine($"NodeAnnotation {fragmentName}Annotation = null;");
            writer.WriteLine($"var {fragmentName}Values = new ValueFragment[] {{}};");
            writer.WriteLine($"var {fragmentName}Children = new StructureFragment[] {{}};");
            writer.WriteLine($"var {fragmentName}FragmentType = FragmentType.Query;");

            writer.WriteLine($"return new {nameof(StructureFragment)}({fragmentName}Name, {fragmentName}Plurality, {fragmentName}Annotation, {fragmentName}Values, {fragmentName}Children, {fragmentName}FragmentType);");
            writer.Indent -= 1;
            writer.WriteLine("}");
            writer.WriteLine($"var {variableName} = Create{fragment.Name}{nameof(StructureFragment)}();");
        }

        private string ToCamelCase(string s)
        {
            var span = new Span<char>(s.ToCharArray());
            span[0] = char.ToLower(span[0]);
            return span.ToString();
        }
    }
}
