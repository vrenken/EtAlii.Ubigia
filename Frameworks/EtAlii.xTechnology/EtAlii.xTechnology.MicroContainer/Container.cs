namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;

    public partial class Container
	{
        private readonly Dictionary<Type, ContainerRegistration> _mappings = new Dictionary<Type, ContainerRegistration>();

        // TODO: A very VERY bad practice.
        public Container(bool registerContainer = false)
        {
            if (registerContainer)
            {
                _mappings.Add(GetType(), new ContainerRegistration
                {
                    ConcreteType = GetType(),
                    Instance = this,
                });
            }
        }

        public Container()
        {
            //_mappings.Add(GetType(), new ContainerRegistration
            //{
            //    ConcreteType = GetType(),
            //    Instance = this,
            //});
        }
	}
}
