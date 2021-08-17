// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    internal interface IPropertiesValueSetter
    {
        Task<Value> Set(string valueName, Structure structure, object value, ExecutionScope scope);
    }
}
