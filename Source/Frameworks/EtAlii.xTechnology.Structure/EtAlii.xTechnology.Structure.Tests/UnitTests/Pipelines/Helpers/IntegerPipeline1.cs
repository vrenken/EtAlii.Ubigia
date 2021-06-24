// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;

    internal class IntegerPipeline1 : Pipeline<int, int>
    {
        public IntegerPipeline1()
        {
            this.StartWith(i => i * 3)
                .ContinueWith(i => i * 2)
                .EndWith(i => i + 4);
        }
    }
}