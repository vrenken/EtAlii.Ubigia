// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

internal class AddOperatorProcessor : IAddOperatorProcessor
{
    private readonly IAddByNameAsNewPathProcessor _addByNameAsNewPathProcessor;
    private readonly IAddRootedPathToExistingPathProcessor _addRootedPathToExistingPathProcessor;
    private readonly IAddAbsolutePathToExistingPathProcessor _addAbsolutePathToExistingPathProcessor;
    private readonly IAddRelativePathToExistingPathProcessor _addRelativePathToExistingPathProcessor;
    private readonly IAddVariableToExistingPathProcessor _addVariableToExistingPathProcessor;
    private readonly IAddConstantToExistingPathProcessor _addConstantToExistingPathProcessor;
    private readonly IAddVariableAsNewPathProcessor _addVariableAsNewPathProcessor;
    private readonly IAddFunctionToExistingPathProcessor _addFunctionToExistingPathProcessor;

    // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
    // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
    // specified by SonarQube. The current setup here is already some kind of facade that hides away many processing specific variations. Therefore refactoring to facades won't work.
    // Therefore this pragma warning disable of S107.
#pragma warning disable S107
    public AddOperatorProcessor(
        IAddByNameAsNewPathProcessor addByNameAsNewPathProcessor,
        IAddRootedPathToExistingPathProcessor addRootedPathToExistingPathProcessor,
        IAddAbsolutePathToExistingPathProcessor addAbsolutePathToExistingPathProcessor,
        IAddRelativePathToExistingPathProcessor addRelativePathToExistingPathProcessor,
        IAddVariableToExistingPathProcessor addVariableToExistingPathProcessor,
        IAddConstantToExistingPathProcessor addConstantToExistingPathProcessor,
        IAddVariableAsNewPathProcessor addVariableAsNewPathProcessor,
        IAddFunctionToExistingPathProcessor addFunctionToExistingPathProcessor)
#pragma warning restore S107
    {
        _addByNameAsNewPathProcessor = addByNameAsNewPathProcessor;
        _addRootedPathToExistingPathProcessor = addRootedPathToExistingPathProcessor;
        _addAbsolutePathToExistingPathProcessor = addAbsolutePathToExistingPathProcessor;
        _addRelativePathToExistingPathProcessor = addRelativePathToExistingPathProcessor;
        _addVariableToExistingPathProcessor = addVariableToExistingPathProcessor;
        _addConstantToExistingPathProcessor = addConstantToExistingPathProcessor;
        _addVariableAsNewPathProcessor = addVariableAsNewPathProcessor;
        _addFunctionToExistingPathProcessor = addFunctionToExistingPathProcessor;
    }

    public Task Process(OperatorParameters parameters)
    {
        return parameters switch
        {
            { } p when p.LeftSubject is not EmptySubject && p.RightSubject is VariableSubject => _addVariableToExistingPathProcessor.Process(parameters),
            {LeftSubject: EmptySubject, RightSubject: VariableSubject} => _addVariableAsNewPathProcessor.Process(parameters),
            {LeftSubject: EmptySubject} => _addByNameAsNewPathProcessor.Process(parameters),
            { } p when p.LeftSubject is not EmptySubject && p.RightSubject is RootedPathSubject => _addRootedPathToExistingPathProcessor.Process(parameters),
            { } p when p.LeftSubject is not EmptySubject && p.RightSubject is RelativePathSubject => _addRelativePathToExistingPathProcessor.Process(parameters),
            { } p when p.LeftSubject is not EmptySubject && p.RightSubject is AbsolutePathSubject => _addAbsolutePathToExistingPathProcessor.Process(parameters),
            { } p when p.LeftSubject is not EmptySubject && p.RightSubject is StringConstantSubject => _addConstantToExistingPathProcessor.Process(parameters),
            { } p when p.LeftSubject is not EmptySubject && p.RightSubject is FunctionSubject => _addFunctionToExistingPathProcessor.Process(parameters),
            _ => throw new NotSupportedException($"Cannot determine add path processor for: {parameters?.ToString() ?? "NULL"}")
        };
    }
}
