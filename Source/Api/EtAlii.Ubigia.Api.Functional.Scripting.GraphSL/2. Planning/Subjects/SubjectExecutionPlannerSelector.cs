﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
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

        public SubjectExecutionPlannerSelector(
            IAbsolutePathSubjectExecutionPlanner absolutePathSubjectExecutionPlanner,
            IRelativePathSubjectExecutionPlanner relativePathSubjectExecutionPlanner,
            IRootedPathSubjectExecutionPlanner rootedPathSubjectExecutionPlanner,
            IConstantSubjectExecutionPlanner constantSubjectExecutionPlanner,
            IVariableSubjectExecutionPlanner variableSubjectExecutionPlanner,
            IFunctionSubjectExecutionPlanner functionSubjectExecutionPlanner,
            IRootSubjectExecutionPlanner rootSubjectExecutionPlanner,
            IRootDefinitionSubjectExecutionPlanner rootDefinitionSubjectExecutionPlanner)
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
}