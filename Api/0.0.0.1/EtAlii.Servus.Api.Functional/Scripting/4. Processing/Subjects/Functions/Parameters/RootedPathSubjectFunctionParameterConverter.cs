﻿namespace EtAlii.Servus.Api.Functional
{
    using System.Reactive.Linq;

    internal class RootedPathSubjectFunctionParameterConverter : IRootedPathSubjectFunctionParameterConverter
    {
        public object Convert(FunctionSubjectArgument argument)
        {
            var constantArgument = (RootedPathFunctionSubjectArgument)argument;
            return Observable
                .Return<object>(constantArgument.Subject)
                .ToHotObservable();
        }
    }
}
