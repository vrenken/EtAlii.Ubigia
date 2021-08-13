// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if USE_ORIGINAL_CONTAINER

//#define CHECK_USAGE
namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This internal class contains the per-instance know-how needed to instantiate one specific object in the DI chain.
    /// </summary>
    /// <remarks>Reason for the usage of members instead of properties is speed - Especially for bigger, more complex container usage.</remarks>
    internal class ContainerRegistration
    {
#if CHECK_USAGE
        /// <summary>
        /// Shows how often an instance has been injected.
        /// </summary>
        public int Usages;
#endif
#if DEBUG
        /// <summary>
        /// Returns true when the instance is being constructed.
        /// This is used to determine cyclic dependencies and other DI misconducts.
        /// </summary>
        public bool UnderConstruction;
#endif

        /// <summary>
        /// The actual instantiated object. Needed as there might be multiple places where it needs to be injected.
        /// </summary>
        public object? Instance;

        /// <summary>
        /// The concrete type to be used for instantiating the object. This member cannot be set together with the
        /// ConstructMethod member.
        /// </summary>
        public Type? ConcreteType;

        /// <summary>
        /// The object constructor function that should be used for instantiating the object. This member cannot be set together with the
        /// ConcreteType member.
        /// </summary>
        public Func<object>? ConstructMethod;

        /// <summary>
        /// Returns true when the lazy initialization for this instance has happened.
        /// </summary>
        public bool IsLazyInitialized;

        /// <summary>
        /// State that represents the list of initializers that should be run immediately after the object has been instantiated.
        /// </summary>
        public readonly List<Action<object>> ImmediateInitializers = new();

        /// <summary>
        /// State that represents the list of initializers that should be run after the root object has been instantiated.
        /// </summary>
        public readonly List<Action<object>> LazyInitializers = new();

        /// <summary>
        /// State that represents the registrations that should be used to decorate the central instance.
        /// </summary>
        public readonly List<DecoratorRegistration> Decorators = new();
    }
}

#endif
