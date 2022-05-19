// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class ScaffoldingImplementation : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IFourthParent, FourthParent>();
        }
    }
}
