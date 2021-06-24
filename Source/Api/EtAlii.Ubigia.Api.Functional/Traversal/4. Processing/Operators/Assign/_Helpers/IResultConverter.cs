// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal interface IResultConverter
    {
        Task Convert(object input, ExecutionScope scope, IObserver<object> output);

    }
}
