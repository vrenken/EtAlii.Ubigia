// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure
{
    public interface IQuery : IParams
    {
    }

    public interface IQuery<out TParam> : IQuery, IParams<TParam>
    {
    }

    public interface IQuery<out TParam1, out TParam2> : IQuery, IParams<TParam1, TParam2>
    {
    }

    public interface IQuery<out TParam1, out TParam2, out TParam3> : IQuery, IParams<TParam1, TParam2, TParam3>
    {
    }
}
