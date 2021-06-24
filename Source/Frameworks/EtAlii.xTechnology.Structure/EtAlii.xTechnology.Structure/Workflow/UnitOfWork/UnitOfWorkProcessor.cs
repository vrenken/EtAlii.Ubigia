// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    public class UnitOfWorkProcessor : IUnitOfWorkProcessor
    {
        public void Process(IUnitOfWork unitOfWork, IUnitOfWorkHandler handler)
        {
            handler.Handle(unitOfWork);
        }
    }
}
