namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal interface ITimePreparer
    {
        void Prepare(IProcessingContext context, ExecutionScope scope, DateTime time);
    }
}