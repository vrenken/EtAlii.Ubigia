namespace EtAlii.Ubigia.Infrastructure.Logical
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Fabric;

	public interface ILogicalContextConfiguration
    {
        string Name { get; }
	    Uri DataApiAddress { get; }

        IFabricContext Fabric { get; }
        
        ILogicalContextConfiguration Use(string name, Uri dataApiAddress);

        ILogicalContextConfiguration Use(IFabricContext fabric);
    }
}