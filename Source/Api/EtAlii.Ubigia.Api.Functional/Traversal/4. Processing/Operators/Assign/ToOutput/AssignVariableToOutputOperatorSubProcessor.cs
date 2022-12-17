// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class AssignVariableToOutputOperatorSubProcessor : IAssignVariableToOutputOperatorSubProcessor
    {
        private readonly IResultConverter _resultConverter;

        public AssignVariableToOutputOperatorSubProcessor(IResultConverter resultConverter)
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
                        var message = "Unable to assign variable items as output";
                        parameters.Output.OnError(new InvalidOperationException(message, e));
                    }
                });
            return Task.CompletedTask;
        }
    }
}
