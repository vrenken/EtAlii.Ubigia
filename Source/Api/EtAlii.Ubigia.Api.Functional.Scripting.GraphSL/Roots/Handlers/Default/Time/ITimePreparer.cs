namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;

    internal interface ITimePreparer
    {
        void Prepare(IScriptProcessingContext context, ExecutionScope scope, DateTime time);
    }
}