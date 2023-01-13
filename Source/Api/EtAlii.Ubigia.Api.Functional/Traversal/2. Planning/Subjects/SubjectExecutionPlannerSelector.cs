// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;

internal class SubjectExecutionPlannerSelector : ISubjectExecutionPlannerSelector
{
    private readonly IAbsolutePathSubjectExecutionPlanner _absolutePathSubjectExecutionPlanner;
    private readonly IRelativePathSubjectExecutionPlanner _relativePathSubjectExecutionPlanner;
    private readonly IRootedPathSubjectExecutionPlanner _rootedPathSubjectExecutionPlanner;
    private readonly IConstantSubjectExecutionPlanner _constantSubjectExecutionPlanner;
    private readonly IVariableSubjectExecutionPlanner _variableSubjectExecutionPlanner;
    private readonly IFunctionSubjectExecutionPlanner _functionSubjectExecutionPlanner;
    private readonly IRootSubjectExecutionPlanner _rootSubjectExecutionPlanner;
    private readonly IRootDefinitionSubjectExecutionPlanner _rootDefinitionSubjectExecutionPlanner;

    // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
    // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
    // specified by SonarQube. The current setup here is already some kind of facade that hides away many planning specific variations. Therefore refactoring to facades won't work.
    // Therefore this pragma warning disable of S107.
#pragma warning disable S107
    public SubjectExecutionPlannerSelector(
        IAbsolutePathSubjectExecutionPlanner absolutePathSubjectExecutionPlanner,
        IRelativePathSubjectExecutionPlanner relativePathSubjectExecutionPlanner,
        IRootedPathSubjectExecutionPlanner rootedPathSubjectExecutionPlanner,
        IConstantSubjectExecutionPlanner constantSubjectExecutionPlanner,
        IVariableSubjectExecutionPlanner variableSubjectExecutionPlanner,
        IFunctionSubjectExecutionPlanner functionSubjectExecutionPlanner,
        IRootSubjectExecutionPlanner rootSubjectExecutionPlanner,
        IRootDefinitionSubjectExecutionPlanner rootDefinitionSubjectExecutionPlanner)
#pragma warning restore S107
    {
        _absolutePathSubjectExecutionPlanner = absolutePathSubjectExecutionPlanner;
        _relativePathSubjectExecutionPlanner = relativePathSubjectExecutionPlanner;
        _rootedPathSubjectExecutionPlanner = rootedPathSubjectExecutionPlanner;
        _constantSubjectExecutionPlanner = constantSubjectExecutionPlanner;
        _variableSubjectExecutionPlanner = variableSubjectExecutionPlanner;
        _functionSubjectExecutionPlanner = functionSubjectExecutionPlanner;
        _rootSubjectExecutionPlanner = rootSubjectExecutionPlanner;
        _rootDefinitionSubjectExecutionPlanner = rootDefinitionSubjectExecutionPlanner;
    }

    public IExecutionPlanner Select(object item)
    {
        return item switch
        {
            AbsolutePathSubject => _absolutePathSubjectExecutionPlanner,
            RootedPathSubject => _rootedPathSubjectExecutionPlanner,
            RelativePathSubject => _relativePathSubjectExecutionPlanner,
            ConstantSubject => _constantSubjectExecutionPlanner,
            VariableSubject => _variableSubjectExecutionPlanner,
            FunctionSubject => _functionSubjectExecutionPlanner,
            RootSubject => _rootSubjectExecutionPlanner,
            RootDefinitionSubject => _rootDefinitionSubjectExecutionPlanner,
            _ => throw new InvalidOperationException($"Unable to select option for criteria: {(item != null ? item.ToString() : "[NULL]")}")
        };
    }
}
