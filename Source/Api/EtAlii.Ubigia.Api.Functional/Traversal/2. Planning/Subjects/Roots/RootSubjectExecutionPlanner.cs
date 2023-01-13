// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

internal class RootSubjectExecutionPlanner : IRootSubjectExecutionPlanner
{
    private readonly IRootSubjectProcessor _processor;

    public RootSubjectExecutionPlanner(IRootSubjectProcessor processor)
    {
        _processor = processor;
    }

    public ISubjectExecutionPlan Plan(SequencePart part)
    {
        var rootSubject = (RootSubject)part;
        return new RootSubjectExecutionPlan(rootSubject, _processor);
    }
}
