// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;

internal class OperatorExecutionPlannerSelector : IOperatorExecutionPlannerSelector
{
    private readonly IAddOperatorExecutionPlanner _addOperatorExecutionPlanner;
    private readonly IAssignOperatorExecutionPlanner _assignOperatorExecutionPlanner;
    private readonly IRemoveOperatorExecutionPlanner _removeOperatorExecutionPlanner;

    public OperatorExecutionPlannerSelector(
        IAddOperatorExecutionPlanner addOperatorExecutionPlanner,
        IAssignOperatorExecutionPlanner assignOperatorExecutionPlanner,
        IRemoveOperatorExecutionPlanner removeOperatorExecutionPlanner)
    {
        _addOperatorExecutionPlanner = addOperatorExecutionPlanner;
        _assignOperatorExecutionPlanner = assignOperatorExecutionPlanner;
        _removeOperatorExecutionPlanner = removeOperatorExecutionPlanner;
    }

    public IExecutionPlanner Select(object item)
    {
        return item switch
        {
            AddOperator => _addOperatorExecutionPlanner,
            AssignOperator => _assignOperatorExecutionPlanner,
            RemoveOperator => _removeOperatorExecutionPlanner,
            _ => throw new InvalidOperationException($"Unable to select option for criteria: {(item != null ? item.ToString() : "[NULL]")}")
        };
    }
}
