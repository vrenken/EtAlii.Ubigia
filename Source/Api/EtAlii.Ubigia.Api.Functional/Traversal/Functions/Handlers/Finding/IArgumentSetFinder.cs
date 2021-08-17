// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IArgumentSetFinder
    {
        ArgumentSet Find(FunctionSubject functionSubject, ExecutionScope scope);
    }
}
