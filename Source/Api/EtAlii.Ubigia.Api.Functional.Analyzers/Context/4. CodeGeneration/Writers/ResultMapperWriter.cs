// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Serilog;

    public sealed class ResultMapperWriter : IResultMapperWriter
    {
        public void Write(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment, bool isRoot)
        {
            var className = isRoot
                ? structureFragment.Name
                : structureFragment.Name + "Type";

            writer.WriteLine($"public class ResultMapper : IResultMapper<{className}>");
            writer.WriteLine("{");
            writer.Indent += 1;
            writer.WriteLine($"public {className} Map(Structure structure)");
            writer.WriteLine("{");
            writer.Indent += 1;

            WriteInstance(writer, structureFragment, isRoot);

            writer.Indent -= 1;
            writer.WriteLine("}");
            writer.Indent -= 1;
            writer.WriteLine("}");
        }

        private void WriteInstance(IndentedTextWriter writer, StructureFragment structureFragment, bool isRoot)
        {
            var className = isRoot
                ? structureFragment.Name
                : structureFragment.Name + "Type";

            writer.WriteLine($"var instance = new {className}();");

            writer.WriteLine();

            WriteChildMappings(writer, structureFragment);

            writer.WriteLine();

            WriteValueMappings(writer, structureFragment);

            writer.WriteLine("return instance;");
        }

        private void WriteChildMappings(IndentedTextWriter writer, StructureFragment structureFragment)
        {
            var pluralChildren = new List<StructureFragment>();
            foreach (var childFragment in structureFragment.Children)
            {
                if (childFragment.Plurality == Plurality.Multiple)
                {
                    writer.WriteLine($"var {ToCamelCase(childFragment.Name)} = new List<{childFragment.Name}Type>();");
                    pluralChildren.Add(childFragment);
                }

                writer.WriteLine($"var {ToCamelCase(childFragment.Name)}Mapper = new {childFragment.Name}Type.ResultMapper();");
            }

            writer.WriteLine("foreach(var child in structure.Children)");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine("switch(child.Type)");
            writer.WriteLine("{");
            writer.Indent += 1;

            foreach (var childFragment in structureFragment.Children)
            {
                writer.WriteLine($"case \"{childFragment.Name}\":");
                writer.Indent += 1;
                if (childFragment.Plurality == Plurality.Single)
                {
                    writer.WriteLine($"instance.{childFragment.Name} = {ToCamelCase(childFragment.Name)}Mapper.Map(child);");
                }
                else
                {
                    writer.WriteLine($"{ToCamelCase(childFragment.Name)}.Add({ToCamelCase(childFragment.Name)}Mapper.Map(child));");
                }
                writer.WriteLine("break;");
                writer.Indent -= 1;
            }

            writer.WriteLine($"default:");
            writer.Indent += 1;
            writer.WriteLine($"throw new InvalidOperationException($\"Unable to map structure child: {{child.Name}}\");");
            writer.Indent -= 1;

            writer.Indent -= 1;
            writer.WriteLine("}");

            writer.Indent -= 1;
            writer.WriteLine("}");

            foreach (var pluralChildName in pluralChildren.Select(c => c.Name))
            {
                writer.WriteLine($"instance.{pluralChildName} = {ToCamelCase(pluralChildName)}.ToArray();");
            }
        }

        private void WriteValueMappings(IndentedTextWriter writer, StructureFragment structureFragment)
        {
            writer.WriteLine("foreach(var value in structure.Values)");
            writer.WriteLine("{");
            writer.Indent += 1;

            writer.WriteLine("switch(value.Name)");
            writer.WriteLine("{");
            writer.Indent += 1;

            foreach (var valueFragment in structureFragment.Values)
            {
                var typeAsString = valueFragment.Prefix.ValueType switch
                {
                    ValueType.Object => "object",
                    ValueType.String => "string",
                    ValueType.Bool => "bool",
                    ValueType.Float => "float",
                    ValueType.Int => "int",
                    ValueType.DateTime => "System.DateTime",
                    ValueType.Guid => "System.Guid",
                    _ => throw new NotSupportedException()
                };

                writer.WriteLine($"case \"{valueFragment.Name}\":");
                writer.Indent += 1;
                writer.WriteLine($"instance.{valueFragment.Name} = ({typeAsString})value.Object;");
                writer.WriteLine("break;");
                writer.Indent -= 1;
            }

            writer.WriteLine($"default:");
            writer.Indent += 1;
            writer.WriteLine($"throw new InvalidOperationException($\"Unable to map structure value: {{value.Name}}\");");
            writer.Indent -= 1;

            writer.Indent -= 1;
            writer.WriteLine("}");

            writer.Indent -= 1;
            writer.WriteLine("}");
        }

        private string ToCamelCase(string s)
        {
            var span = new Span<char>(s.ToCharArray());
            span[0] = char.ToLower(span[0]);
            return span.ToString();
        }
    }
}
