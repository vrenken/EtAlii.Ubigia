namespace EtAlii.xTechnology.MicroContainer
{
	using System;
	using System.Collections.Generic;

    /// <summary>
    /// This container is build to be simple and pure. We don't want to assign too much 'lifetime' responsibilities
    /// to our DI framework. Most lifetime behavior can be solved way easier without a container.
    /// </summary>
	public partial class Container
	{
        private readonly Dictionary<Type, ContainerRegistration> _mappings = new();
	}
}
