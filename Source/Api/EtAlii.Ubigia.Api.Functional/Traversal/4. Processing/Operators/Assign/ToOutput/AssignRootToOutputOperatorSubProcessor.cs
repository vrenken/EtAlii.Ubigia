// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

internal class AssignRootToOutputOperatorSubProcessor : IAssignRootToOutputOperatorSubProcessor
{
    public Task Assign(OperatorParameters parameters)
    {
        parameters.RightInput.Subscribe(
            onError: (e) => parameters.Output.OnError(e),
            onCompleted: () => parameters.Output.OnCompleted(),
            onNext: o => parameters.Output.OnNext(o));
        return Task.CompletedTask;
    }
}
