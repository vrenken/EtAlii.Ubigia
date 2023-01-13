// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

internal class ConstantSubjectExecutionPlan : SubjectExecutionPlanBase
{
    private readonly IConstantSubjectProcessor _processor;

    public ConstantSubjectExecutionPlan(
        ConstantSubject subject,
        IConstantSubjectProcessor processor)
        : base(subject)
    {
        _processor = processor;
    }

    protected override Type GetOutputType()
    {
        return Subject.GetType();
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
