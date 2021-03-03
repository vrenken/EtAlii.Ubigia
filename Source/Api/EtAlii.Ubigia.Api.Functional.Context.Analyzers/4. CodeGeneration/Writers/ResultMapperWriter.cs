// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System;
    using System.CodeDom.Compiler;
    using Serilog;
    using ValueType = EtAlii.Ubigia.Api.Functional.Context.ValueType;

    public class ResultMapperWriter : IResultMapperWriter
    {
        public void Write(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment, bool isRoot)
        {
            var className = isRoot
                ? structureFragment.Name
                : structureFragment.Name + "Type";

            writer.WriteLine($"public class ResultMapper : IResultMapper<{className}>");
            writer.WriteLine("{");
            writer.Indent += 1;
            writer.WriteLine($"public {className} MapRoot(Structure structure)");
            writer.WriteLine("{");
            writer.Indent += 1;

            WriteInstance(logger, writer, structureFragment, isRoot);

            writer.Indent -= 1;
            writer.WriteLine("}");
            writer.Indent -= 1;
            writer.WriteLine("}");
        }

        private void WriteInstance(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment, bool isRoot)
        {
            var className = isRoot
                ? structureFragment.Name
                : structureFragment.Name + "Type";

            writer.WriteLine($"var instance = new {className}();");

            writer.WriteLine("foreach(var child in structure.Children)");
            writer.WriteLine("{");
            writer.Indent += 1;

            // switch case to do mapping.

            writer.Indent -= 1;
            writer.WriteLine("}");

            writer.WriteLine("foreach(var value in structure.Values)");
            writer.WriteLine("{");
            writer.Indent += 1;

            WriteValueMappings(logger, writer, structureFragment);

            writer.Indent -= 1;
            writer.WriteLine("}");

            writer.WriteLine("return instance;");

            // Structure s;
            // s.Children.
            // s.Children;
            // s.Name;
            // s.Values[].Object
        }

        private void WriteValueMappings(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment)
        {
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
        }
    }
}
