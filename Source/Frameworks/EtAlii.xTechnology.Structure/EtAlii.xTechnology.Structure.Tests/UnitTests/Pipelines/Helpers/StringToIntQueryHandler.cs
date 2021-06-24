// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    public class StringToIntQueryHandler : IQueryHandler<StringToIntQuery, string, int>
    {
        public StringToIntQuery Create(string parameter)
        {
            return new(parameter);
        }

        public int Handle(StringToIntQuery query)
        {
            return query.String.Length;
        }
    }
}
