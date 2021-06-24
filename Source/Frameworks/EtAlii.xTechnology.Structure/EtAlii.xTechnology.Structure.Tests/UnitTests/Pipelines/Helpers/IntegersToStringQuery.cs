// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    public class IntegersToStringQuery : Query<int, int>
    {
        public IntegersToStringQuery(int parameter1, int parameter2) 
            : base(parameter1, parameter2)
        {
        }

        public int Integer1 => ((IParams<int, int>)this).Parameter1;
        public int Integer2 => ((IParams<int, int>)this).Parameter2;
    }
}