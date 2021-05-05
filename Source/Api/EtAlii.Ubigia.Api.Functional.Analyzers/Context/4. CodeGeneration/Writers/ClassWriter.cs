// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.CodeDom.Compiler;
    using Serilog;

    public class ClassWriter : IClassWriter
    {
        private readonly IPropertyWriter _propertyWriter;
        private readonly IAnnotationCommentWriter _annotationCommentWriter;
        private readonly IResultMapperWriter _resultMapperWriter;

        public ClassWriter(
            IPropertyWriter propertyWriter,
            IAnnotationCommentWriter annotationCommentWriter,
            IResultMapperWriter resultMapperWriter)
        {
            _propertyWriter = propertyWriter;
            _annotationCommentWriter = annotationCommentWriter;
            _resultMapperWriter = resultMapperWriter;
        }

        public void Write(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment)
        {
            Write(logger, writer, structureFragment, true);
        }

        private void Write(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment, bool isRoot)
        {
            var className = isRoot
                ? structureFragment.Name
                : structureFragment.Name + "Type";
            logger
                .ForContext("StructureFragment", structureFragment, true)
                .Information("Writing class: {ClassName}", className);

            _annotationCommentWriter.Write(logger, writer, structureFragment.Annotation);

            writer.WriteLine($"public partial class {className}");
            writer.WriteLine("{");
            writer.Indent += 1;

            for (var i = 0; i < structureFragment.Values.Length; i++)
            {
                var valueFragment = structureFragment.Values[i];
                _propertyWriter.Write(logger, writer, valueFragment);
                if (i < structureFragment.Values.Length - 1)
                {
                    writer.WriteLine();
                }
            }

            for (var i = 0; i < structureFragment.Children.Length; i++)
            {
                var nestedStructureFragment = structureFragment.Children[i];
                var nestedClassName = nestedStructureFragment.Name;

                _annotationCommentWriter.Write(logger, writer, nestedStructureFragment.Annotation);

                var plurality = nestedStructureFragment.Plurality == Plurality.Multiple ? "[]" : "";
                writer.WriteLine($"public {nestedClassName}Type{plurality} {nestedClassName} {{ get; private set;}}");

                writer.WriteLine();

                Write(logger, writer, nestedStructureFragment, false);

                if (i < structureFragment.Children.Length - 1)
                {
                    writer.WriteLine();
                }
            }

            writer.WriteLine();

            _resultMapperWriter.Write(logger, writer, structureFragment, isRoot);

            writer.Indent -= 1;
            writer.WriteLine("}");
        }
    }
}
