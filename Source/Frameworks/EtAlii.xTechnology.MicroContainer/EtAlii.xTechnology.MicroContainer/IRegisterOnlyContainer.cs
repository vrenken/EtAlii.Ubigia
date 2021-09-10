// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer
{
    using System;

    public interface IRegisterOnlyContainer
    {
        /// <summary>
        /// Register an concrete implementation type to be instantiated wherever the service interface is being used as
        /// a constructor parameter.
        /// </summary>
        /// <remarks>In contrast with the 'default' Microsoft DI patterns this container enforces registrations to be made on interfaces. Additionally (and intentionally) the container also only
        /// allows singleton registrations. Scoped and transient registrations are not possible.</remarks>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type has already been registered or when the service type is not an interface.</exception>
        void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Register an object instantiation function that will be used to provide the concrete instance wherever the service interface is being used as
        /// a constructor parameter.
        /// </summary>
        /// <remarks>In contrast with the 'default' Microsoft DI patterns this container enforces registrations to be made on interfaces. Additionally (and intentionally) the container also only
        /// allows singleton registrations. Scoped and transient registrations are not possible.</remarks>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type has already been registered or when the service type is not an interface.</exception>
        void Register<TService, TImplementation>(Func<IServiceCollection, TImplementation> constructMethod)
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Register an object instantiation function that will be used to provide the concrete instance wherever the service interface is being used as
        /// a constructor parameter.
        /// </summary>
        /// <remarks>In contrast with the 'default' Microsoft DI patterns this container enforces registrations to be made on interfaces. Additionally (and intentionally) the container also only
        /// allows singleton registrations. Scoped and transient registrations are not possible.</remarks>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type has already been registered or when the service type is not an interface.</exception>
        void Register<TService, TImplementation>(Func<TImplementation> constructMethod)
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Register an object instantiation function that will be used to provide an instance wherever the service interface is being used as
        /// a constructor parameter.
        /// </summary>
        /// <remarks>In contrast with the 'default' Microsoft DI patterns this container enforces registrations to be made on interfaces. Additionally (and intentionally) the container also only
        /// allows singleton registrations. Scoped and transient registrations are not possible.</remarks>
        /// <typeparam name="TService"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type has already been registered or when the service type is not an interface.</exception>
        void Register<TService>(Func<IServiceCollection, TService> constructMethod)
            where TService : class;

        /// <summary>
        /// Register an object instantiation function that will be used to provide an instance wherever the service interface is being used as
        /// a constructor parameter.
        /// </summary>
        /// <remarks>In contrast with the 'default' Microsoft DI patterns this container enforces registrations to be made on interfaces. Additionally (and intentionally) the container also only
        /// allows singleton registrations. Scoped and transient registrations are not possible.</remarks>
        /// <typeparam name="TService"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type has already been registered or when the service type is not an interface.</exception>
        void Register<TService>(Func<TService> constructMethod)
            where TService : class;

        /// <summary>
        /// Registers a decorator that will wrap the concrete instance. This is very useful for conditional logic and
        /// 'meta-behavior' like conditional profiling/logging/debugging.
        /// </summary>
        /// <remarks>In contrast with the 'default' Microsoft DI patterns this container enforces registrations to be made on interfaces. Additionally (and intentionally) the container also only
        /// allows singleton registrations. Scoped and transient registrations are not possible.</remarks>
        /// <exception cref="InvalidOperationException">In case the decorator type has already been registered, does not have a service instance constructor parameter or when the service type is not an interface.</exception>
        void RegisterDecorator<TService, TDecorator>()
            where TService : class
            where TDecorator : TService;

        bool HasRegistration<TService>()
            where TService : class;

        /// <summary>
        /// Registers an initializer that will be called right after an object has been constructed.
        /// This is useful and often needed for creating bidirectional object access which by theory are not possible in a normal DI tree.
        /// </summary>
        /// <remarks>In contrast with the 'default' Microsoft DI patterns this container enforces registrations to be made on interfaces. Additionally (and intentionally) the container also only
        /// allows singleton registrations. Scoped and transient registrations are not possible.</remarks>
        /// <param name="initializer"></param>
        /// <typeparam name="TService"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type is not an interface.</exception>
        void RegisterInitializer<TService>(Action<TService> initializer);

        /// <summary>
        /// Registers an initializer that will be called right after an object has been constructed.
        /// This is useful and often needed for creating bidirectional object access which by theory are not possible in a normal DI tree.
        /// </summary>
        /// <remarks>In contrast with the 'default' Microsoft DI patterns this container enforces registrations to be made on interfaces. Additionally (and intentionally) the container also only
        /// allows singleton registrations. Scoped and transient registrations are not possible.</remarks>
        /// <param name="initializer"></param>
        /// <typeparam name="TService"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type is not an interface.</exception>
        void RegisterInitializer<TService>(Action<IServiceCollection, TService> initializer);
    }
}
