// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class CheckFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        public CheckFunctionHandler()
        {
            ParameterSets = new[]
            {
                new ParameterSet(true),
                new ParameterSet(false, new Parameter("var", typeof (object))),
            };
            Name = "Check";
        }

        public Task Process(
            IFunctionContext context,
            ParameterSet parameterSet,
            ArgumentSet argumentSet,
            IObservable<object> input,
            ExecutionScope scope,
            IObserver<object> output,
            bool processAsSubject)
        {
            output.OnCompleted();
            return Task.CompletedTask;
        }
    }
}
