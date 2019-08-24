﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Threading.Tasks;

    internal class ObjectConstantSubjectProcessor : IObjectConstantSubjectProcessor
    {
        public Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var value = ((ObjectConstantSubject)subject).Values;
            output.OnNext(value);
            output.OnCompleted();
            
            return Task.CompletedTask;
        }
    }
}
