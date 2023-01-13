// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Reactive.Linq;

internal class RootedPathSubjectFunctionParameterConverter : IRootedPathSubjectFunctionParameterConverter
{
    public object Convert(FunctionSubjectArgument argument, ExecutionScope scope)
    {
        var constantArgument = (RootedPathFunctionSubjectArgument)argument;
        return Observable
            .Return<object>(constantArgument.Subject)
            .ToHotObservable();
    }
}
