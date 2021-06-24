// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class StringConstantSubjectProcessor : IStringConstantSubjectProcessor
    {
        public Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var value = ((StringConstantSubject)subject).Value;
            output.OnNext(value);
            output.OnCompleted();

            return Task.CompletedTask;
        }
    }
}
