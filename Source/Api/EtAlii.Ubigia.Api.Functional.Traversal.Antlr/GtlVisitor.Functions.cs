// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class GtlVisitor
    {
        public override object VisitSubject_function(GtlParser.Subject_functionContext context)
        {
            var parts = context
                .GetText()
                .TrimEnd(')')
                .Split('(');
            var functionName = parts[0];
            var arguments = parts[1].Length == 0
                    ? Array.Empty<FunctionSubjectArgument>()
                    : parts[1]
                    .Split(',')
                    .Select(parameterName => new ConstantFunctionSubjectArgument(parameterName))
                    .Cast<FunctionSubjectArgument>()
                    .ToArray();

            return new FunctionSubject(functionName, arguments);
        }
    }
}
