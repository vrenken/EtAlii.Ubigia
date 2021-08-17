// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal class AssignPathToOutputOperatorSubProcessor : IAssignPathToOutputOperatorSubProcessor
    {
        private readonly IResultConverter _resultConverter;

        public AssignPathToOutputOperatorSubProcessor(IResultConverter resultConverter)
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
                    await _resultConverter.Convert(o, parameters.Scope, parameters.Output).ConfigureAwait(false);
                });
            return Task.CompletedTask;
        }
    }
}
