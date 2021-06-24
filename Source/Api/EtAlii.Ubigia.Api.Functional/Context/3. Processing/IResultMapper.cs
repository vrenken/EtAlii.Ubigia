// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    public interface IResultMapper<out TResult>
    {
        TResult Map(Structure structure);
    }
}
