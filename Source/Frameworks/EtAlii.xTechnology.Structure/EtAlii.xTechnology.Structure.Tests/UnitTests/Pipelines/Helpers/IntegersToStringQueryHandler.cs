// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    public class IntegersToStringQueryHandler : IQueryHandler<IntegersToStringQuery, int, int, string>
    {
        public IntegersToStringQuery Create(int parameter1, int parameter2)
        {
            return new(parameter1, parameter2);
        }

        public string Handle(IntegersToStringQuery query)
        {
            return $"{query.Integer1}-{query.Integer2}";
        }
    }
}
