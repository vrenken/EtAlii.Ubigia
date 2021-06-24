// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerToArrayQueryHandler : IQueryHandler<IntegerToArrayQuery, int, int[]>
    {
        public IntegerToArrayQuery Create(int parameter)
        {
            return new(parameter);
        }

        public int[] Handle(IntegerToArrayQuery query)
        {
            var result = new int[query.Integer];
            for (var i = 0; i < query.Integer; i++)
            {
                result[i] = i;
            }
            return result;
        }
    }
}
