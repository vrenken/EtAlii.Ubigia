// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public static class PropertyChangedEventHandlerSetAndRaise
{
    public static void SetAndRaise<T>(this PropertyChangedEventHandler handler, object sender, ref T storage, T newValue, [CallerMemberName] string propertyName = null)
    {
        if (!Equals(storage, newValue))
        {
            storage = newValue;
            handler?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}
