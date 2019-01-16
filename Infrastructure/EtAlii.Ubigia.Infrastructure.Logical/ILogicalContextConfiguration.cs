namespace EtAlii.Ubigia.Infrastructure.Logical
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Fabric;

    public interface ILogicalContextConfiguration
    {
        string Name { get; }
	    Uri Address { get; }

        IFabricContext Fabric { get; }

        //ILogicalContextExtension[] Extensions { get; }

        //ILogicalContextConfiguration Use(ILogicalContextExtension[] extensions);

        ILogicalContextConfiguration Use(string name, Uri address);

        ILogicalContextConfiguration Use(IFabricContext fabricContext);
    }
}