// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

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

    public ArgumentSet Find(FunctionSubject functionSubject, ExecutionScope scope)
    {
        var arguments = functionSubject.Arguments
            .Select(argument =>
            {
                return argument switch
                {
                    NonRootedPathFunctionSubjectArgument nonRootedPathFunctionSubjectArgument => _nonRootedPathSubjectFunctionParameterConverter.Convert(nonRootedPathFunctionSubjectArgument, scope),
                    RootedPathFunctionSubjectArgument rootedPathFunctionSubjectArgument => _rootedPathSubjectFunctionParameterConverter.Convert(rootedPathFunctionSubjectArgument, scope),
                    ConstantFunctionSubjectArgument constantFunctionSubjectArgument => _constantSubjectFunctionParameterConverter.Convert(constantFunctionSubjectArgument, scope),
                    VariableFunctionSubjectArgument variableFunctionSubjectArgument => _variableSubjectFunctionParameterConverter.Convert(variableFunctionSubjectArgument, scope),
                    _ => throw new NotSupportedException($"Cannot process argument: {argument?.ToString() ?? "NULL"}")
                };
            })
            .ToArray();
        return new ArgumentSet(arguments);
    }
}
