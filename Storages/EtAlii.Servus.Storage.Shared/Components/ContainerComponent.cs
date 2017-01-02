namespace EtAlii.Servus.Storage
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;
using EtAlii.Servus.Client.Model;

    [JsonObject(MemberSerialization.Fields)]
    public abstract class ContainerComponent
    {
        private static readonly Dictionary<Type, string> _containerComponentNames = new Dictionary<Type, string>();
        private static readonly object[] _emptyConstructorParameters = new object[] { };

        internal ContainerComponent()
        {
        }

        internal abstract string Name { get; }

        public static string GetName(ContainerComponent containerComponent)
        {
            return containerComponent.Name;
        }

        public static string GetName<T>()
            where T : ContainerComponent
        {
            var type = typeof(T);
            var name = String.Empty;
            
            if (!_containerComponentNames.TryGetValue(type, out name))
            {
                var constructors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                var constructor = constructors.First(c => c.GetParameters().Length == 0);
                var instance = (T)constructor.Invoke(_emptyConstructorParameters);
                name = instance.Name;
                _containerComponentNames[type] = name;
            }
            return name;
        }

        public abstract void Apply(IEditableEntry entry);
    }
}
