// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class AssignTagToOutputOperatorSubProcessor : IAssignTagToOutputOperatorSubProcessor
    {
        public Task Assign(OperatorParameters parameters)
        {
            parameters.RightInput.Subscribe(
                onError: (e) => parameters.Output.OnError(e),
                onCompleted: () => parameters.Output.OnCompleted(),
                onNext: o =>
                {
                    var entry = ((IInternalNode)o).Entry;
                    parameters.Output.OnNext(entry.Tag);
                });
            return Task.CompletedTask;
        }
    }
}
