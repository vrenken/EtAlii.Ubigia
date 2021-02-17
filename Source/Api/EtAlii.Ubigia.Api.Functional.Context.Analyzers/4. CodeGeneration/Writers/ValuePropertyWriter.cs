// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System;
    using System.CodeDom.Compiler;
    using Serilog;
    using ValueType = EtAlii.Ubigia.Api.Functional.Context.ValueType;

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
                .ForContext("ValueFragment", valueFragment, true)
                .ForContext("Annotation", annotation?.ToString())
                .ForContext("Prefix", prefix.ToString())
                .Information("Writing property: {Property}", propertyName);

            // var requirementAsString = prefix.Requirement switch
            // {
            //     Requirement.None => "",
            //     Requirement.Mandatory => "!",
            //     Requirement.Optional => "?",
            //     _ => throw new NotSupportedException()
            // };
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
            var valueAsString = prefix.ValueType switch
            {
                ValueType.Object => "new object()",
                ValueType.String => $"\"{annotation?.ToString()?.Replace("\\", "\\\\") ?? ""}\"",
                ValueType.Bool => "true",
                ValueType.Float => "42.42f",
                ValueType.Int => "42",
                ValueType.DateTime => "System.DateTime.Now",
                _ => throw new NotSupportedException()
            };

            _annotationCommentWriter.Write(logger, writer, annotation);

            writer.WriteLine($"public {typeAsString} {propertyName} {{get;}} = {valueAsString};");
        }

    }
}
