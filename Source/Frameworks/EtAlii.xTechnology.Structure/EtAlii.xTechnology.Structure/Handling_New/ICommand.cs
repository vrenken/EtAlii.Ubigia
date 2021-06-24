// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure
{
    public interface ICommand : IParams
    {
    }

    public interface ICommand<out TParam> : ICommand, IParams<TParam>
    {
    }

    public interface ICommand<out TParam1, out TParam2> : ICommand, IParams<TParam1, TParam2>
    {
    }

    public interface ICommand<out TParam1, out TParam2, out TParam3> : ICommand, IParams<TParam1, TParam2, TParam3>
    {
    }
}
