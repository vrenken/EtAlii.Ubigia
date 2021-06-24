// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    public class IntegerArrayToIntQuery : Query<int[]>
    {
        public IntegerArrayToIntQuery(int[] parameter) 
            : base(parameter)
        {
        }

        public int[] Array => ((IParams<int[]>)this).Parameter;
    }
}