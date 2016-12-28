namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal interface ITimePreparer
    {
        void Prepare(IRootContext context, ExecutionScope scope, DateTime dateTime);
    }
}