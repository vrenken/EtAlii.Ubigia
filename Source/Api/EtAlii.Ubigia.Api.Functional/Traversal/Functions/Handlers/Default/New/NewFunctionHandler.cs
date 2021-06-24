// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class NewFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        public NewFunctionHandler()
        {
            ParameterSets = new[]
            {
                new ParameterSet(false),
                new ParameterSet(false, new Parameter("name", typeof(string)))
            };
            Name = "New";
        }

        public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            if (processAsSubject)
            {
                if (argumentSet.Arguments.Length == 0)
                {
                    var id = Guid.NewGuid();
                    output.OnNext($"Undefined_{Base36Convert.ToString(id)}");
                }
                else if (argumentSet.Arguments.Length == 1)
                {
                    if (argumentSet.Arguments[0] is string name)
                    {
                        output.OnNext(name);
                    }
                    else
                    {
                        throw new ScriptProcessingException("Unable to convert name argument for New function processing");
                    }
                }
                else
                {
                    throw new ScriptProcessingException("Too many arguments for New function processing");
                }
                output.OnCompleted();
            }
            else
            {
                // We can only use the new function as a subject. It cannot be used as a pipeline to funnel other items through.
                throw new ScriptProcessingException("Unable to use input data for New function processing");
            }
            return Task.CompletedTask;
        }
    }
}
