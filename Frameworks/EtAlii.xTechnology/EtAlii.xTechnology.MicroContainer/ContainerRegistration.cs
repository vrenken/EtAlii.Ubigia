namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Collections.Generic;

    internal class ContainerRegistration
    {
/*
#if DEBUG
        public int Usages;
#endif
*/
        public object Instance;
        public Type ConcreteType;
        public Func<object> ConstructMethod;
        public readonly List<Action<object>> Initializers = new List<Action<object>>();
        public readonly List<DecoratorRegistration> Decorators = new List<DecoratorRegistration>();
    }
}
