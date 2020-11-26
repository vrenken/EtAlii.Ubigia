//#define CHECK_USAGE
namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Collections.Generic;

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

        public object Instance;
        public Type ConcreteType;
        public bool IsLazyInitialized;
        public Func<object> ConstructMethod;
        public readonly List<Action<object>> ImmediateInitializers = new();
        public readonly List<Action<object>> LazyInitializers = new();
        public readonly List<DecoratorRegistration> Decorators = new();
    }
}
