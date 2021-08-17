// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    public abstract class ExecutionPlan
    {
        internal ExecutionPlanResultSink ResultSink { get; private set; }

        internal abstract Task Execute(ExecutionScope scope);

        internal void SetResultSink(ExecutionPlanResultSink resultSink)
        {
            ResultSink = resultSink;
        }
    }
}
