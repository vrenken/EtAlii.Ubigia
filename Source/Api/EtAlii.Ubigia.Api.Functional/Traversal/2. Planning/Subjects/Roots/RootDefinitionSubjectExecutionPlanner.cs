// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

internal class RootDefinitionSubjectExecutionPlanner : IRootDefinitionSubjectExecutionPlanner
{
    private readonly IRootDefinitionSubjectProcessor _processor;

    public RootDefinitionSubjectExecutionPlanner(IRootDefinitionSubjectProcessor processor)
    {
        _processor = processor;
    }

    public ISubjectExecutionPlan Plan(SequencePart part)
    {
        var rootDefinitionSubject = (RootDefinitionSubject)part;
        return new RootDefinitionSubjectExecutionPlan(rootDefinitionSubject, _processor);
    }
}
