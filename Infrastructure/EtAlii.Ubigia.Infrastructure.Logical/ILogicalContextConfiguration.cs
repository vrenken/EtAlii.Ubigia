namespace EtAlii.Ubigia.Infrastructure.Logical
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Fabric;

	public interface ILogicalContextConfiguration
    {
        string Name { get; }
	    Uri Address { get; }

        IFabricContext Fabric { get; }
        
        ILogicalContextConfiguration Use(string name, Uri address);

        ILogicalContextConfiguration Use(IFabricContext fabric);
    }
}