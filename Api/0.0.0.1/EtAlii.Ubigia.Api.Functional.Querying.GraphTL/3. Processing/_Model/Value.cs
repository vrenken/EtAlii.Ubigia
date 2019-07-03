﻿namespace EtAlii.Ubigia.Api.Functional 
{
    public sealed class Value
    {
        public string Name { get; }
        public object Object { get; }

        public Value(string name, object @object)
        {
            Name = name;
            Object = @object;
        }
    }
}
