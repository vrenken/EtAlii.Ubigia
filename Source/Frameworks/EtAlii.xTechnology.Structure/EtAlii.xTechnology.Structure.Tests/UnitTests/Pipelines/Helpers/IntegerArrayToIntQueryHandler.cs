// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    public class IntegerArrayToIntQueryHandler : IQueryHandler<IntegerArrayToIntQuery, int[], int>
    {
        public IntegerArrayToIntQuery Create(int[] parameter)
        {
            return new(parameter);
        }

        public int Handle(IntegerArrayToIntQuery query)
        {
            return query.Array.Sum();
        }
    }
}
