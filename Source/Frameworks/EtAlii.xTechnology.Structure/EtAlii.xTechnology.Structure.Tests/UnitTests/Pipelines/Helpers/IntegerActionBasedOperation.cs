// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure.Pipelines;

    public class IntegerActionBasedOperation : IOperation<int, int>
    {
        public int Process(int input)
        {
            return input * 3;
        }
    }
}