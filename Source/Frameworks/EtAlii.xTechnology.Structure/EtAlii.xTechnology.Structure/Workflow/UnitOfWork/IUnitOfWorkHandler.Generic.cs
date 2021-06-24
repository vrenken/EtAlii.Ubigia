// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    // ReSharper disable once UnusedTypeParameter
    public interface IUnitOfWorkHandler<TUnitOfWork> : IUnitOfWorkHandler
        where TUnitOfWork : IUnitOfWork
    {
    }
}
