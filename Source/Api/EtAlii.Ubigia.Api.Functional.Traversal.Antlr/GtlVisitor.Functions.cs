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
            var functionName = (string)VisitIdentifier(context.identifier());
            var argumentContexts = context.subject_function_argument();
            var arguments = argumentContexts.Length == 0
                    ? Array.Empty<FunctionSubjectArgument>()
                    : argumentContexts
                    .Select(argumentContext => (FunctionSubjectArgument)Visit(argumentContext))
                    .ToArray();

            return new FunctionSubject(functionName, arguments);
        }

        public override object VisitSubject_function_argument_identifier(GtlParser.Subject_function_argument_identifierContext context)
        {
            var text = context.GetText();
            return new ConstantFunctionSubjectArgument(text);

        }

        public override object VisitSubject_function_argument_string_quoted(GtlParser.Subject_function_argument_string_quotedContext context)
        {
            var text = (string)VisitString_quoted(context.string_quoted());
            return new ConstantFunctionSubjectArgument(text);
        }

        public override object VisitSubject_function_argument_rooted_path(GtlParser.Subject_function_argument_rooted_pathContext context)
        {
            var root = (string)VisitIdentifier(context.identifier());
            var rootedPathSubject = BuildRootedPathSubject(root, context.path_part());

            return new RootedPathFunctionSubjectArgument(rootedPathSubject);
        }

        public override object VisitSubject_function_argument_non_rooted_path(GtlParser.Subject_function_argument_non_rooted_pathContext context)
        {
            var subject = BuildNonRootedPathSubject(context.path_part());

            return subject switch
            {
                StringConstantSubject stringConstantSubject => new ConstantFunctionSubjectArgument(stringConstantSubject.Value),
                VariableSubject variableSubject => new VariableFunctionSubjectArgument(variableSubject.Name),
                NonRootedPathSubject nonRootedPathSubject => new NonRootedPathFunctionSubjectArgument(nonRootedPathSubject),
                RootedPathSubject rootedPathSubject => new RootedPathFunctionSubjectArgument(rootedPathSubject),
                _ => throw new ScriptParserException($"The function subject argument could not be understood: {context.GetText()}" )
            };
        }

        public override object VisitSubject_function_argument_variable(GtlParser.Subject_function_argument_variableContext context)
        {
            var variableName = (string)VisitIdentifier(context.identifier());
            return new VariableFunctionSubjectArgument(variableName);
        }
    }
}
