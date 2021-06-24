// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerToArrayCommandHandler : ICommandHandler<IntegerToArrayCommand, int>
    {
        public IntegerToArrayCommand Create(int parameter)
        {
            return new(parameter);
        }

        public void Handle(IntegerToArrayCommand command)
        {
        }
    }
}
