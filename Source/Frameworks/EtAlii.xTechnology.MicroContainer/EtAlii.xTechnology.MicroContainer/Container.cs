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
        // This mapping contains the understanding of all the objects that the container can instantiate.
        // It not only contains concrete type registrations but also construction methods, initializer know-how
        // and the possibility to decorate objects.   
        private readonly Dictionary<Type, ContainerRegistration> _mappings = new();
	}
}
