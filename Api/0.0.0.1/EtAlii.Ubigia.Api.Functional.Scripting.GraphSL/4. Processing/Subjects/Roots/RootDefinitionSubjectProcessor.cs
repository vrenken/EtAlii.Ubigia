﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class RootDefinitionSubjectProcessor : IRootDefinitionSubjectProcessor
    {
        //private readonly IPathSubjectForOutputConverter _converter;

        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var rootDefinitionSubject = (RootDefinitionSubject) subject;
            output.OnNext(rootDefinitionSubject);
            output.OnCompleted();

            await Task.CompletedTask;
        }
    }
}
