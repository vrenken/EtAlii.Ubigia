namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;
    using System.Reflection;

    public partial class Container
    {
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

        // TODO: This method should be private.
        //[Obsolete("This method should be private")]
        public object GetInstance(Type type)
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
                    if (mapping.ConcreteType == null && mapping.ConstructMethod == null)
                    {
                        throw new InvalidOperationException("Unable to create instance for type: " + type);
                    }

                    var newInstance = mapping.ConstructMethod == null 
                        ? CreateInstance(mapping.ConcreteType, null, null) 
                        : mapping.ConstructMethod();

                    newInstance = DecorateInstanceIfNeeded(mapping, newInstance);
                    mapping.Instance = newInstance;
                    InitializeInstance(mapping, mapping.Instance);
                }
                instance = mapping.Instance;
            }
            else
            {
                throw new InvalidOperationException("No mapping found for type: " + type);
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

        private object CreateInstance(Type type, Type serviceTypeToReplace, object instanceToReplaceServiceTypeWith)
        {
            var constructors = type.GetTypeInfo().DeclaredConstructors
                .Where(c => !c.IsStatic && c.IsPublic)
                .ToArray();//.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (constructors.Length > 1)
            {
                throw new InvalidOperationException("Multiple public constructors are not allowed for type: " + type);
            }
            if (constructors.Length == 0)
            {
                throw new InvalidOperationException("No constructors found for type: " + type);
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
                throw new InvalidOperationException("Unable to create instance of type: " + type + " due to issues with parameter: " + parameterIndex + "\r\n", e);
            }

        }
    }
}