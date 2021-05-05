namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    internal interface ITimePreparer
    {
        void Prepare(IScriptProcessingContext context, ExecutionScope scope, DateTime time);
    }
}
