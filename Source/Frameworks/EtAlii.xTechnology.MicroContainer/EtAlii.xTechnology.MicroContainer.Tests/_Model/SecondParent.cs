// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public class SecondParent : ISecondParent
    {
        public object Instance { get; }
        
        public SecondParent()
        {
            Instance = new object();
        }
    }
}
