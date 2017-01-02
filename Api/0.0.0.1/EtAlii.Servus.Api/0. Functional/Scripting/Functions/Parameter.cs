namespace EtAlii.Servus.Api.Functional
{
    using System;

    public class Parameter
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public Type Type { get { return _type; } }
        private readonly Type _type;

        public Parameter(string name, Type type)
        {
            _name = name;
            _type = type;
        }
    }
}