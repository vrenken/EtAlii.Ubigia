// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;

public class SequenceProcessingResult
{
    /// <summary>
    /// The Sequence for which results will be returned through the Output observable.
    /// </summary>
    public Sequence Sequence { get; }
    public ISequenceExecutionPlan ExecutionPlan { get;  }
    public int Step { get; }
    public int Total { get; }

    /// <summary>
    /// The observable output for the specified sequence. Make sure to await with LastOrDefaultAsync if you are not
    /// sure results will be returned.
    /// </summary>
    public IObservable<object> Output { get; }

    public SequenceProcessingResult(
        Sequence sequence,
        ISequenceExecutionPlan executionPlan,
        int step,
        int total,
        IObservable<object> output)
    {
        Sequence = sequence;
        ExecutionPlan = executionPlan;
        Step = step;
        Total = total;
        Output = output;
    }
}
