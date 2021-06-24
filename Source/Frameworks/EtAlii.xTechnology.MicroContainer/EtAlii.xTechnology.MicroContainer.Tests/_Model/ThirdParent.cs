// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class ThirdParent : IThirdParent
    {
        public object Instance { get; private set; }

        public void Initialize(object instance)
        {
            Instance = instance;
        }
    }
}
