// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.CodeDom.Compiler;
    using Serilog;

    public class ValuePropertyWriter : IPropertyWriter
    {
        private readonly IAnnotationCommentWriter _annotationCommentWriter;

        public ValuePropertyWriter(IAnnotationCommentWriter annotationCommentWriter)
        {
            _annotationCommentWriter = annotationCommentWriter;
        }

        public void Write(ILogger logger, IndentedTextWriter writer, ValueFragment valueFragment)
        {
            var propertyName = valueFragment.Name;

            var prefix = valueFragment.Prefix;
            var annotation = valueFragment.Annotation;

            logger
                .ForContext("ValueFragment", valueFragment.Name)
                .ForContext("Annotation", annotation?.ToString())
                .ForContext("Prefix", prefix.ToString())
                .Information("Writing property: {Property}", propertyName);

            var typeAsString = prefix.ValueType switch
            {
                ValueType.Object => "object",
                ValueType.String => "string",
                ValueType.Bool => "bool",
                ValueType.Float => "float",
                ValueType.Int => "int",
                ValueType.DateTime => "System.DateTime",
                _ => throw new NotSupportedException()
            };

            _annotationCommentWriter.Write(logger, writer, annotation);

            writer.WriteLine($"public {typeAsString} {propertyName} {{get; private set;}}");
        }

    }
}
