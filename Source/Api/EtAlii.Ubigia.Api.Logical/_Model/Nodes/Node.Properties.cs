// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public partial class Node
    {
        private readonly PropertyDictionary _properties;

        PropertyDictionary IInternalNode.GetProperties()
        {
            return _properties;
        }

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected T GetProperty<T>([CallerMemberName] string propertyName = null)
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
        protected bool SetProperty<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            var oldValue = GetProperty<T>(propertyName);

            if (Equals(oldValue, newValue)) return false;

            _properties[propertyName!] = newValue;
            NotifyPropertyChanged(this, oldValue, newValue, propertyName);

            MarkAsModified();

            return true;
        }

        private void MarkAsModified()
        {
            var wasModified = _isModified;
            _isModified = true;
            NotifyPropertyChanged(this, wasModified, _isModified, nameof(INode.IsModified));
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        /// <param name="sender"></param>
        /// <param name="oldValue"></param>
        protected virtual void NotifyPropertyChanged(object sender, object oldValue, object newValue, [CallerMemberName] string propertyName = null)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                eventHandler(this, e);
            }
        }
    }
}
