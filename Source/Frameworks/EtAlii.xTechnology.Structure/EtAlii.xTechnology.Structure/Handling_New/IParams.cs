// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure
{
    public interface IParams
    {
    }

    public interface IParams<out TParam> : IParams
    {
        TParam Parameter { get; }
    }

    public interface IParams<out TParam1, out TParam2> : IParams
    {
        TParam1 Parameter1 { get; }
        TParam2 Parameter2 { get; }
    }

    public interface IParams<out TParam1, out TParam2, out TParam3> : IParams
    {
        TParam1 Parameter1 { get; }
        TParam2 Parameter2 { get; }
        TParam3 Parameter3 { get; }
    }
}
