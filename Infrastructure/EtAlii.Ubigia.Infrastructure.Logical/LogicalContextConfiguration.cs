namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        //public ILogicalContextExtension[] Extensions { get { return _extensions; } }
        //private ILogicalContextExtension[] _extensions

        public IFabricContext Fabric { get; private set; }

        public string Name { get; private set; }

        public Uri Address { get; private set; }

        //public ILogicalContextConfiguration Use(ILogicalContextExtension[] extensions)
        //[
        //    if (extensions == null)
        //    [
        //        throw new ArgumentException(nameof(extensions))
        //    ]
        //    _extensions = extensions
        //        .Concat(_extensions)
        //        .Distinct()
        //        .ToArray()
        //    return this
        //]
        public ILogicalContextConfiguration Use(string name, Uri address)
        {
			Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));

            return this;
        }

        public ILogicalContextConfiguration Use(IFabricContext fabric)
        {
			Fabric = fabric ?? throw new ArgumentException(nameof(fabric));

            return this;
        }
    }
}
