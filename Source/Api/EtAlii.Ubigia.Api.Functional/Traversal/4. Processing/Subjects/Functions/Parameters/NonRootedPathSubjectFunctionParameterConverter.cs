// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Reactive.Linq;

    internal class NonRootedPathSubjectFunctionParameterConverter : INonRootedPathSubjectFunctionParameterConverter
    {
        public object Convert(FunctionSubjectArgument argument, ExecutionScope scope)
        {
            var constantArgument = (NonRootedPathFunctionSubjectArgument)argument;
            return Observable
                .Return<object>(constantArgument.Subject)
                .ToHotObservable();
        }
    }
}
