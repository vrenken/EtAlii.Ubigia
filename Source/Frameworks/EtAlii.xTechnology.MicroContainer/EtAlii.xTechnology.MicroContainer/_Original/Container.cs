// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
	using System;
	using System.Collections.Generic;

    /// <summary>
    /// This container is build to be simple and pure. We don't want to assign too much 'lifetime' responsibilities
    /// to our DI framework. Most lifetime behavior can be solved way easier without a container.
    /// </summary>
	public partial class Container : IRegisterOnlyContainer, IServiceCollection
	{
        // This mapping contains the understanding of all the objects that the container can instantiate.
        // It not only contains concrete type registrations but also construction methods, initializer know-how
        // and the possibility to decorate objects.
        private readonly Dictionary<Type, ContainerRegistration> _mappings = new();
	}
}

#endif
