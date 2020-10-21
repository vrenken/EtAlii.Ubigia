namespace EtAlii.xTechnology.MicroContainer
{
	using System;
	using System.Collections.Generic;

	public partial class Container
	{
        private readonly Dictionary<Type, ContainerRegistration> _mappings = new Dictionary<Type, ContainerRegistration>();
	}
}
