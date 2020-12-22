namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    public class Parameter
    {
        public string Name { get; }

        public Type Type { get; }

        public Parameter(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
