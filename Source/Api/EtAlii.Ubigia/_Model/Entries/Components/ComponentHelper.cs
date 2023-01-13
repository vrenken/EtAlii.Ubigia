// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

public static class ComponentHelper
{
    private static readonly ConcurrentDictionary<Type, string> _componentNames = new();
    private static readonly object[] _emptyConstructorParameters = Array.Empty<object>();

    // Can ComponentHelper.GetName be completely replaced by the Generic GetName method?
    // More details can be found in the Github issue below:
    // https://github.com/vrenken/EtAlii.Ubigia/issues/77
    public static string GetName(IComponent containerComponent)
    {
        return ((ComponentBase)containerComponent).Name;
    }

    public static string GetName<T>()
        where T : ComponentBase
    {
        var type = typeof(T);

        if (!_componentNames.TryGetValue(type, out var name))
        {
            var constructor = type.GetTypeInfo()
                .DeclaredConstructors
                .First(c => !c.IsStatic && c.GetParameters().Length == 0);
            var instance = (T)constructor.Invoke(_emptyConstructorParameters);
            name = instance.Name;
            _componentNames[type] = name;
        }
        return name;
    }

    public static void SetId(CompositeComponent compositeComponent, ulong id)
    {
        compositeComponent.Id = id;
    }

    public static void SetStored(IComponent component, bool stored)
    {
        ((ComponentBase)component).Stored = stored;
    }
}
