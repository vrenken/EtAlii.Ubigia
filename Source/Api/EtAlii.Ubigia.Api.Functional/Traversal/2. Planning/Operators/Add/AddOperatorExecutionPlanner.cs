﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

internal class AddOperatorExecutionPlanner : IAddOperatorExecutionPlanner
{
    private readonly IAddOperatorProcessor _processor;

    public AddOperatorExecutionPlanner(IAddOperatorProcessor processor)
    {
        _processor = processor;
    }

    public IScriptExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right)
    {
        return new AddOperatorExecutionPlan(left, right, _processor);
    }
}
