namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        //public ILogicalContextExtension[] Extensions { get { return _extensions; } }
        //private ILogicalContextExtension[] _extensions;

        public IFabricContext Fabric => _fabric;
        private IFabricContext _fabric;

        public string Name => _name;
        private string _name;

        public string Address => _address;
        private string _address;

        //public ILogicalContextConfiguration Use(ILogicalContextExtension[] extensions)
        //{
        //    if (extensions == null)
        //    {
        //        throw new ArgumentException(nameof(extensions));
        //    }

        //    _extensions = extensions
        //        .Concat(_extensions)
        //        .Distinct()
        //        .ToArray();
        //    return this;
        //}

        public ILogicalContextConfiguration Use(string name, string address)
        {
            if (name == null)
            {
                throw new ArgumentException(nameof(name));
            }

            if (address == null)
            {
                throw new ArgumentException(nameof(address));
            }

            _name = name;
            _address = address;

            return this;
        }

        public ILogicalContextConfiguration Use(IFabricContext fabric)
        {
            if (fabric == null)
            {
                throw new ArgumentException(nameof(fabric));
            }

            _fabric = fabric;

            return this;
        }
    }
}
