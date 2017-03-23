namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        //public ILogicalContextExtension[] Extensions { get { return _extensions; } }
        //private ILogicalContextExtension[] _extensions;

        public IFabricContext Fabric { get; private set; }

        public string Name { get; private set; }

        public string Address { get; private set; }

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

            Name = name;
            Address = address;

            return this;
        }

        public ILogicalContextConfiguration Use(IFabricContext fabric)
        {
            if (fabric == null)
            {
                throw new ArgumentException(nameof(fabric));
            }

            Fabric = fabric;

            return this;
        }
    }
}
