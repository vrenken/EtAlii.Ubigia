namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal interface ITimePreparer
    {
        void Prepare(IScriptProcessingContext context, ExecutionScope scope, DateTime time);
    }
}