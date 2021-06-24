// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal interface IPathValueSetter
    {
        Task<Value> Set(string valueName, string value, Structure structure, PathSubject path, SchemaExecutionScope executionScope);
    }
}
