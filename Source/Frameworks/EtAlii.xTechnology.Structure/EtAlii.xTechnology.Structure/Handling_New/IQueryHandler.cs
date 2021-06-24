// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure
{
    public interface IQueryHandler<TQuery, out TResult>
        where TQuery : IQuery
    {
        TQuery Create();
        TResult Handle(TQuery query);
    }

    public interface IQueryHandler<TQuery, in TParam, out TResult>
        where TQuery: IQuery<TParam>
    {
        TQuery Create(TParam parameter); 
        TResult Handle(TQuery query);
    }

    public interface IQueryHandler<TQuery, in TParam1, in TParam2, out TResult>
        where TQuery : IQuery<TParam1, TParam2>
    {
        TQuery Create(TParam1 parameter1, TParam2 parameter2);
        TResult Handle(TQuery query);
    }

    public interface IQueryHandler<TQuery, in TParam1, in TParam2, in TParam3, out TResult>
        where TQuery : IQuery<TParam1, TParam2, TParam3>
    {
        TQuery Create(TParam1 parameter1, TParam2 parameter2, TParam3 param3);
        TResult Handle(TQuery query);
    }
}
