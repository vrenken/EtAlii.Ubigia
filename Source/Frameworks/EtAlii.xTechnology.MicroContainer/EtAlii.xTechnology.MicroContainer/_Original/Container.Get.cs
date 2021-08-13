// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if USE_ORIGINAL_CONTAINER

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
        ///
        /// The container works in a way that it tries to create the constructor-injected objects dependency tree.
        /// During this creation it will run all requested initializations immediately after the construction of
        /// a single object, and after the root object has been created it will run all lazy registrations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">In case one of the objects in the DI graph cannot be initialized.
        /// For example due to a missing registration or when a cyclic dependency has been defined.</exception>

        public T GetInstance<T>()
        {
#if CHECK_USAGE
            // This check below can be activated to find out which container registrations are not needed.
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

            // After the requested object has been instantiated we need to make sure the lazy initialization
            // request are taken care of.
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
                // This check ensures that cyclic dependencies (either through constructor or initializer access) are caught
                // when calling the GetInstance method.
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
                    // We need to indicate when the instance in the mapping is under construction.
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

        /// <summary>
        /// Create an instance for the specified type. This requires instantiating its constructor parameters as well.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceTypeToReplace"></param>
        /// <param name="instanceToReplaceServiceTypeWith"></param>
        /// <param name="involvedContainerRegistrations"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private object CreateInstance(Type? type, Type? serviceTypeToReplace, object? instanceToReplaceServiceTypeWith, List<ContainerRegistration> involvedContainerRegistrations)
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
                        instances[parameterIndex] = instanceToReplaceServiceTypeWith!;
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
                throw new InvalidOperationException($"Unable to create instance of type: {type} due to issues with parameter: {parameterIndex}", e);
            }
        }
    }
}

#endif
