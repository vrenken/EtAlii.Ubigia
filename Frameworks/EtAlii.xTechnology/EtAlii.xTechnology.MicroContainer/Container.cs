namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;

    public class Container
	{
        private readonly Dictionary<Type, ContainerRegistration> _mappings = new Dictionary<Type, ContainerRegistration>();

        public Container()
        {
            //_mappings.Add(GetType(), new ContainerRegistration
            //{
            //    ConcreteType = GetType(),
            //    Instance = this,
            //});
        }

        public void Register<TService, TImplementation>()
            where TImplementation : TService
        {
            var serviceType = typeof(TService);
            if (_mappings.ContainsKey(serviceType))
            {
                throw new InvalidOperationException("Service Type already registered: " + serviceType.ToString());
            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface");
            }

            _mappings[serviceType] = new ContainerRegistration
            {
                ConcreteType = typeof(TImplementation),
            };
        }

        public void Register<TService, TImplementation>(Func<TImplementation> constructMethod)
            where TImplementation : TService
        {
            var serviceType = typeof(TService);
            if (_mappings.ContainsKey(serviceType))
            {
                throw new InvalidOperationException("Service Type already registered");
            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface");
            }

            _mappings[serviceType] = new ContainerRegistration
            {
                ConstructMethod = () => constructMethod(),
                ConcreteType = typeof(TImplementation),
            };
        }

        public void Register<TService>(Func<TService> constructMethod)
        {
            var serviceType = typeof(TService);
            if (_mappings.ContainsKey(serviceType))
            {
                throw new InvalidOperationException("Service Type already registered");
            
            }
            var ti = serviceType.GetTypeInfo();
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface");
            }

            _mappings[serviceType] = new ContainerRegistration
            {
                ConstructMethod = () => constructMethod(),
                ConcreteType = typeof(TService),
            };
        }

        public void RegisterDecorator(Type serviceType, Type decoratorType)
	    {
            if (!_mappings.ContainsKey(serviceType))
            {
                throw new InvalidOperationException("Service Type not yet registered: " + serviceType.ToString());
            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface");
            }
            if (_mappings[serviceType].Decorators.Any(d => d.DecoratorType == decoratorType))
            {
                throw new InvalidOperationException("Decorator Type already registered: " + decoratorType.ToString());
            }

            if (!serviceType.GetTypeInfo().IsAssignableFrom(decoratorType.GetTypeInfo()))
            {
                throw new InvalidOperationException("Unable to apply Decorator Type to Service Type: " + decoratorType.ToString());
            }

            var decoratorRegistration = new DecoratorRegistration
            {
                DecoratorType = decoratorType,
                ServiceType = serviceType,
            };
            _mappings[serviceType].Decorators.Add(decoratorRegistration);
        }

        public void RegisterInitializer<T>(Action<T> initializer)
        {
            var serviceType = typeof(T);
            if (!_mappings.ContainsKey(serviceType))
            {
                throw new InvalidOperationException("Service Type not registered: " + serviceType.ToString());
            }
            var typeInfo = serviceType.GetTypeInfo();
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface");
            }

            _mappings[serviceType].Initializers.Add(o => initializer((T)o));
        }
        
        public T GetInstance<T>()
        {
            var result = (T)GetInstance(typeof(T));
/*
#if DEBUG
            var unusedMappings = _mappings
                .Where(kvp => kvp.Value.Usages == 0)
                .ToArray();
            if (unusedMappings.Any())
            {
                var mappings = String.Join("\r\n", unusedMappings.Select(kvp => kvp.Key.FullName));
                throw new InvalidOperationException("Unused container registrations found: " + mappings);
            }
#endif
*/
            return result;
        }

        private object GetInstance(Type type)
        {
            object instance;

            ContainerRegistration mapping;
            if (_mappings.TryGetValue(type, out mapping))
            {
/*
#if DEBUG
                mapping.Usages += 1;
#endif
*/
                if (mapping.Instance == null)
                {
                    var newInstance = CreateInstance(mapping);
                    newInstance = DecorateInstanceIfNeeded(mapping, newInstance);
                    mapping.Instance = newInstance;
                    InitializeInstance(mapping, mapping.Instance);
                }
                instance = mapping.Instance;
            }
            else
            {
                throw new InvalidOperationException("No mapping found for type: " + type.ToString());
            }
            return instance;
        }

        private object DecorateInstanceIfNeeded(ContainerRegistration mapping, object instance)
        {
            foreach (var decoratorMapping in mapping.Decorators)
            {
                if (mapping.Instance == null)
                {
                    var newInstance = CreateInstance(decoratorMapping.DecoratorType, decoratorMapping.ServiceType, instance);
                    mapping.Instance = newInstance;
                }
                instance = mapping.Instance;
            }
            return instance;
        }

        private void InitializeInstance(ContainerRegistration mapping, object instance)
        {
            foreach (var initializer in mapping.Initializers)
            {
                initializer(instance);
            }
        }

        private object CreateInstance(ContainerRegistration mapping)
        {
            object instance = mapping.ConstructMethod == null ? CreateInstance(mapping.ConcreteType) : mapping.ConstructMethod();
            return instance;
        }

        private object CreateInstance(Type type)
        {
            return CreateInstance(type, null, null);
        }

        private object CreateInstance(Type type, Type serviceTypeToReplace, object instanceToReplaceServiceTypeWith)
        {
            var constructors = type.GetTypeInfo().DeclaredConstructors
                .Where(c => !c.IsStatic)
                .ToArray();//.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (constructors.Length > 1)
            {
                throw new InvalidOperationException("Multiple constructors are not allowed for type: " + type.ToString());
            }
            if (constructors.Length == 0)
            {
                throw new InvalidOperationException("No constructors found for type: " + type.ToString());
            }

            var constructor = constructors[0];
            var parameters = constructor.GetParameters();

            var instances = new object[parameters.Length];

            int parameterIndex = 0;
            try
            {
                for (parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
                {
                    var parameterType = parameters[parameterIndex].ParameterType;
                    if (parameterType == serviceTypeToReplace)
                    {
                        instances[parameterIndex] = instanceToReplaceServiceTypeWith;
                    }
                    else
                    {
                        instances[parameterIndex] = GetInstance(parameterType);
                    }
                }
                return constructor.Invoke(instances);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to create instance of type: " + type.ToString() + " due to issues with parameter: " + parameterIndex + "\r\n", e);
            }

        }
	}
}
