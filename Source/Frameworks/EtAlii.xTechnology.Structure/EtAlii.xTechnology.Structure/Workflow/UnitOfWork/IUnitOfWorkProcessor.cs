// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    public interface IUnitOfWorkProcessor
    {
        void Process(IUnitOfWork unitOfWork, IUnitOfWorkHandler handler);
    }
}