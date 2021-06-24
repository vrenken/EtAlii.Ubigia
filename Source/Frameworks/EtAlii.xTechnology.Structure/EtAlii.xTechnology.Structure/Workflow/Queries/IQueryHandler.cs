// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

using System.Linq;
namespace EtAlii.xTechnology.Structure.Workflow
{
    public interface IQueryHandler<TResult>
    {
        IQueryable<TResult> Handle(IQuery<TResult> query);
    }
}
