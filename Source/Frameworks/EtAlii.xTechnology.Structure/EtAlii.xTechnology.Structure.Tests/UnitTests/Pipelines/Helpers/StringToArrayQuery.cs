// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class StringToArrayQuery : Query<string>
    {
        public StringToArrayQuery(string parameter) 
            : base(parameter)
        {
        }

        public string String => ((IParams<string>)this).Parameter;
    }
}