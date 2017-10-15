namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using EtAlii.Servus.Infrastructure.Fabric;
    using EtAlii.Servus.Storage;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        //public ILogicalContextExtension[] Extensions { get { return _extensions; } }
        //private ILogicalContextExtension[] _extensions;

        public IFabricContext Fabric { get { return _fabric; } }
        private IFabricContext _fabric;

        public string Name { get { return _name; } }
        private string _name;

        public string Address { get { return _address; } }
        private string _address;

        public LogicalContextConfiguration()
        {
            //_extensions = new ILogicalContextExtension[0];
        }

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
