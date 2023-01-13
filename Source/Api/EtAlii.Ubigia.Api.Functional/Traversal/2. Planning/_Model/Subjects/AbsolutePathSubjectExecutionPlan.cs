// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

internal class AbsolutePathSubjectExecutionPlan : SubjectExecutionPlanBase
{
    private readonly IAbsolutePathSubjectProcessor _processor;

    public AbsolutePathSubjectExecutionPlan(AbsolutePathSubject subject, IAbsolutePathSubjectProcessor processor)
        : base (subject)
    {
        _processor = processor;
    }

    protected override Type GetOutputType()
    {
        return typeof (AbsolutePathSubject);
    }

    protected override Task Execute(ExecutionScope scope, IObserver<object> output)
    {
        return _processor.Process(Subject, scope, output);
    }

    public override string ToString()
    {
        return Subject.ToString();
    }
}
