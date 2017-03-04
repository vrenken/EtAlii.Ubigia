namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class Parameter
    {
        public string Name => _name;
        private readonly string _name;

        public Type Type => _type;
        private readonly Type _type;

        public Parameter(string name, Type type)
        {
            _name = name;
            _type = type;
        }
    }
}