// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class AssignEmptyToOutputOperatorSubProcessor : IAssignEmptyToOutputOperatorSubProcessor
    {
        private readonly IResultConverter _resultConverter;

        public AssignEmptyToOutputOperatorSubProcessor(IResultConverter resultConverter)
        {
            _resultConverter = resultConverter;
        }

        public Task Assign(OperatorParameters parameters)
        {
            parameters.RightInput.SubscribeAsync(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: async o =>
                {
                    try
                    {
                        await _resultConverter.Convert(o, parameters.Scope, parameters.Output).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        var message = "Unable to assign empty output";
                        parameters.Output.OnError(new InvalidOperationException(message, e));
                    }
                });
            return Task.CompletedTask;
        }
    }
}
