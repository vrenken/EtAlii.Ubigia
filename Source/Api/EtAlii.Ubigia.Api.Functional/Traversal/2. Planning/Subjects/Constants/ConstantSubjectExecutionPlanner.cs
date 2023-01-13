// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;

internal class ConstantSubjectExecutionPlanner : IConstantSubjectExecutionPlanner
{
    private readonly IStringConstantSubjectProcessor _stringConstantSubjectProcessor;
    private readonly IObjectConstantSubjectProcessor _objectConstantSubjectProcessor;

    public ConstantSubjectExecutionPlanner(
        IStringConstantSubjectProcessor stringConstantSubjectProcessor,
        IObjectConstantSubjectProcessor objectConstantSubjectProcessor)
    {
        _stringConstantSubjectProcessor = stringConstantSubjectProcessor;
        _objectConstantSubjectProcessor = objectConstantSubjectProcessor;
    }

    public ISubjectExecutionPlan Plan(SequencePart part)
    {
        return part switch
        {
            StringConstantSubject stringConstantSubject => new ConstantSubjectExecutionPlan(stringConstantSubject, _stringConstantSubjectProcessor),
            ObjectConstantSubject objectConstantSubject => new ConstantSubjectExecutionPlan(objectConstantSubject, _objectConstantSubjectProcessor),
            _ => throw new NotSupportedException($"Cannot plan constant subject {part?.ToString() ?? "NULL"}")
        };
    }
}
