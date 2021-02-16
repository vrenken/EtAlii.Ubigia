// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.CodeDom.Compiler;
    using Serilog;

    public class ClassWriter : IClassWriter
    {
        private readonly IPropertyWriter _propertyWriter;

        public ClassWriter(IPropertyWriter propertyWriter)
        {
            _propertyWriter = propertyWriter;
        }

        public void Write(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment)
        {
            var className = structureFragment.Name;
            logger
                .ForContext("StructureFragment", structureFragment, true)
                .Information("Writing class: {ClassName}", className);
            writer.WriteLine($"public partial class {className}");
            writer.WriteLine("{");
            writer.Indent += 1;

            foreach (var valueFragment in structureFragment.Values)
            {
                _propertyWriter.Write(logger, writer, valueFragment);
            }

            writer.Indent -= 1;
            writer.WriteLine("}");
        }
    }
}
