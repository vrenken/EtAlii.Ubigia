namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;

    internal class ArgumentSetFinder : IArgumentSetFinder
    {
        private readonly INonRootedPathSubjectFunctionParameterConverter _nonRootedPathSubjectFunctionParameterConverter;
        private readonly IRootedPathSubjectFunctionParameterConverter _rootedPathSubjectFunctionParameterConverter;
        private readonly IConstantSubjectFunctionParameterConverter _constantSubjectFunctionParameterConverter;
        private readonly IVariableSubjectFunctionParameterConverter _variableSubjectFunctionParameterConverter;

        public ArgumentSetFinder(
            INonRootedPathSubjectFunctionParameterConverter nonRootedPathSubjectFunctionParameterConverter,
            IRootedPathSubjectFunctionParameterConverter rootedPathSubjectFunctionParameterConverter,
            IConstantSubjectFunctionParameterConverter constantSubjectFunctionParameterConverter,
            IVariableSubjectFunctionParameterConverter variableSubjectFunctionParameterConverter)
        {
            _nonRootedPathSubjectFunctionParameterConverter = nonRootedPathSubjectFunctionParameterConverter;
            _rootedPathSubjectFunctionParameterConverter = rootedPathSubjectFunctionParameterConverter;
            _constantSubjectFunctionParameterConverter = constantSubjectFunctionParameterConverter;
            _variableSubjectFunctionParameterConverter = variableSubjectFunctionParameterConverter;
        }

        public ArgumentSet Find(FunctionSubject functionSubject)
        {
            var arguments = functionSubject.Arguments
                .Select(argument =>
                {
                    return argument switch
                    {
                        NonRootedPathFunctionSubjectArgument nonRootedPathFunctionSubjectArgument => _nonRootedPathSubjectFunctionParameterConverter.Convert(nonRootedPathFunctionSubjectArgument),
                        RootedPathFunctionSubjectArgument rootedPathFunctionSubjectArgument => _rootedPathSubjectFunctionParameterConverter.Convert(rootedPathFunctionSubjectArgument),
                        ConstantFunctionSubjectArgument constantFunctionSubjectArgument => _constantSubjectFunctionParameterConverter.Convert(constantFunctionSubjectArgument),
                        VariableFunctionSubjectArgument variableFunctionSubjectArgument => _variableSubjectFunctionParameterConverter.Convert(variableFunctionSubjectArgument),
                        _ => throw new NotSupportedException($"Cannot process argument: {argument?.ToString() ?? "NULL"}")
                    };
                })
                .ToArray();
            return new ArgumentSet(arguments);
        }
    }
}
