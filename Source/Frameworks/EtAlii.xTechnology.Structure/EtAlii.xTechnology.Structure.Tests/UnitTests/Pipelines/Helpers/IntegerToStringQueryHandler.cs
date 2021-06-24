// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    public class IntegerToStringQueryHandler : IQueryHandler<IntegerToStringQuery, int, string>
    {
        public IntegerToStringQuery Create(int parameter)
        {
            return new(parameter);
        }

        public string Handle(IntegerToStringQuery query)
        {
            return query.Integer.ToString();
        }
    }
}
