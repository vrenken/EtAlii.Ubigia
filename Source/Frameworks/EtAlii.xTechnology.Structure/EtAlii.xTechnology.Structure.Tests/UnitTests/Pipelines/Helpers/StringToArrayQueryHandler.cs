// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class StringToArrayQueryHandler : IQueryHandler<StringToArrayQuery, string, string[]>
    {
        public StringToArrayQuery Create(string parameter)
        {
            return new(parameter);
        }

        public string[] Handle(StringToArrayQuery query)
        {
            var length = query.String.Length;
            var result = new string[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = query.String.Substring(0, i);
            }
            return result;
        }
    }
}
