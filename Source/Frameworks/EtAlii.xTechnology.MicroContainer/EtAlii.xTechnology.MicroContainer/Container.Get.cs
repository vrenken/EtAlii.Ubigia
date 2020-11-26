//#define CHECK_USAGE
namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public partial class Container
    {
        /// <summary>
        /// Instantiates and returns an instance that got configured for the specified interface.
        /// If the instance requires any constructor parameters these will also get instantiated and injected. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetInstance<T>()
        {
#if CHECK_USAGE
            var unusedMappings = _mappings
                .Where(kvp => kvp.Value.Usages == 0)
                .ToArray();
            if (unusedMappings.Any())
            {
                var mappings = string.Join("\r\n", unusedMappings.Select(kvp => kvp.Key.FullName));
                throw new InvalidOperationException("Unused container registrations found: " + mappings);
            }
#endif

            var involvedContainerRegistrations = new List<ContainerRegistration>();
            var type = typeof(T);
            var instance = (T)GetInstance(type, involvedContainerRegistrations);
            foreach (var involvedContainerRegistration in involvedContainerRegistrations)
            {
                InitializeLazy(involvedContainerRegistration);
            }
            return instance;
        }

        private object GetInstance(Type type, List<ContainerRegistration> involvedContainerRegistrations)
        {
            if (_mappings.TryGetValue(type, out var mapping))
            {
#if DEBUG
                if (mapping.UnderConstruction)
                {
                    throw new InvalidOperationException($"Cyclic dependency detected. Check your registrations and initializations for type: {type}");
                }
#endif
#if CHECK_USAGE
                mapping.Usages += 1;
#endif
                if (mapping.Instance == null)
                {
                    if (mapping.ConcreteType == null && mapping.ConstructMethod == null)
                    {
                        throw new InvalidOperationException($"Unable to create instance for type: {type}");
                    }

#if DEBUG
                    mapping.UnderConstruction = true;
#endif
                    var instance = mapping.ConstructMethod == null 
                        ? CreateInstance(mapping.ConcreteType, null, null, involvedContainerRegistrations) 
                        : mapping.ConstructMethod();
                    mapping.Instance = DecorateInstanceIfNeeded(mapping, instance, involvedContainerRegistrations);
#if DEBUG
                    mapping.UnderConstruction = false;
#endif
                    InitializeImmediately(mapping);
                    involvedContainerRegistrations.Add(mapping);
                }
                return mapping.Instance;
            }
            throw new InvalidOperationException($"No mapping found for type: {type}");
        }

        private object DecorateInstanceIfNeeded(ContainerRegistration mapping, object instance, List<ContainerRegistration> involvedContainerRegistrations)
        {
            foreach (var decoratorMapping in mapping.Decorators)
            {
                if (mapping.Instance == null)
                {
                    var newInstance = CreateInstance(decoratorMapping.DecoratorType, decoratorMapping.ServiceType, instance, involvedContainerRegistrations);
                    mapping.Instance = newInstance;
                }
                instance = mapping.Instance;
            }
            return instance;
        }

        private void InitializeImmediately(ContainerRegistration mapping)
        {
            foreach (var initializer in mapping.ImmediateInitializers)
            {
                initializer(mapping.Instance);
            }
        }

        private void InitializeLazy(ContainerRegistration mapping)
        {
            if (!mapping.IsLazyInitialized)
            {
                mapping.IsLazyInitialized = true;
                foreach (var lazyInitializer in mapping.LazyInitializers)
                {
                    lazyInitializer.Invoke(mapping.Instance);
                }
            }
        }

        private object CreateInstance(Type type, Type serviceTypeToReplace, object instanceToReplaceServiceTypeWith, List<ContainerRegistration> involvedContainerRegistrations)
        {
            var constructors = type.GetTypeInfo().DeclaredConstructors
                .Where(c => !c.IsStatic && c.IsPublic)
                .ToArray();

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

            var parameterIndex = 0;
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
                        instances[parameterIndex] = GetInstance(parameterType, involvedContainerRegistrations);
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
