// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    internal interface ITimePreparer
    {
        void Prepare(IScriptProcessingContext context, ExecutionScope scope, DateTime time);
    }
}
