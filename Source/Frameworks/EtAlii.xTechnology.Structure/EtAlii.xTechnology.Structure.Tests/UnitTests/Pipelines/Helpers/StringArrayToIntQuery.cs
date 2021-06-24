// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    public class StringArrayToIntQuery : Query<string[]>
    {
        public StringArrayToIntQuery(string[] parameter) 
            : base(parameter)
        {
        }

        public string[] Array => ((IParams<string[]>)this).Parameter;
    }
}