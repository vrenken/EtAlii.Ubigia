// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Runtime.CompilerServices;

    public partial class Node
    {
        private const string NotSupportedErrorMessage = "This action is not supported on DynamicNode instances";

         /// <summary>
         /// The properties that make up the node.
         /// </summary>
         public PropertyDictionary Properties => _properties;
         private readonly PropertyDictionary _properties;

         public override bool TryGetMember(GetMemberBinder binder, out object result)
         {
             return _properties.TryGetValue(binder.Name, out result);
         }

         public override bool TrySetMember(SetMemberBinder binder, object value)
         {
             if (_properties.TryGetValue(binder.Name, out var existingValue))
             {
                 _properties[binder.Name] = value;
                 if (value != existingValue)
                 {
                     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(binder.Name));
                 }
             }
             else
             {
                 _properties[binder.Name] = value;
                 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(binder.Name));
             }
             return true;
         }

         /// <summary>
         /// Multicast event for property change notifications.
         /// </summary>
         public event PropertyChangedEventHandler PropertyChanged;


         public T GetProperty<T>([CallerMemberName] string propertyName = null)
         {
             var oldValue = default(T);
             if (_properties.TryGetValue(propertyName!, out var value))
             {
                 oldValue = (T)value;
             }
             return oldValue;
         }

         /// <summary>
         /// Checks if a property already matches a desired value.  Sets the property and
         /// notifies listeners only when necessary.
         /// </summary>
         /// <typeparam name="T">Type of the property.</typeparam>
         /// <param name="newValue">Desired value for the property.</param>
         /// <param name="propertyName">Name of the property used to notify listeners.  This
         /// value is optional and can be provided automatically when invoked from compilers that
         /// support CallerMemberName.</param>
         /// <returns>True if the value was changed, false if the existing value matched the
         /// desired value.</returns>
         public bool SetProperty<T>(T newValue, [CallerMemberName] string propertyName = null)
         {
             var oldValue = GetProperty<T>(propertyName);

             if (Equals(oldValue, newValue)) return false;

             _properties[propertyName!] = newValue;

             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

             return true;
         }

         #region Not supported actions.

         public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
         {
             throw new NotSupportedException(NotSupportedErrorMessage);
         }

         public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
         {
             throw new NotSupportedException(NotSupportedErrorMessage);
         }

         public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
         {
             throw new NotSupportedException(NotSupportedErrorMessage);
         }

         #endregion Not supported actions.
    }
}
