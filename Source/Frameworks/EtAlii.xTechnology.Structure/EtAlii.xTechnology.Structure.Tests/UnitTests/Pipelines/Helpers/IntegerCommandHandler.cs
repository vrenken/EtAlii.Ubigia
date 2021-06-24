// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerCommandHandler : ICommandHandler<IntegerCommand, int>
    {
        public IntegerCommand Create(int parameter)
        {
            return new(parameter);
        }

        public void  Handle(IntegerCommand command)
        {
        }
    }
}
