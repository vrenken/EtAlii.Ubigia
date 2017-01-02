namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public partial class Node : IInternalNode, INode, IEquatable<Node>
    {
        private IDictionary<string, object> _properties = new Dictionary<string, object>();

        IDictionary<string, object> IInternalNode.GetProperties()
        {
            return _properties;
        }

        //void IInternalNode.SetProperties(IDictionary<string, object> properties)
        //{
        //    if (properties == null)
        //    {
        //        throw new ArgumentNullException("properties");
        //    }
        //    _properties = properties;
        //}

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected T GetProperty<T>([CallerMemberName] string propertyName = null)
        {
            var oldValue = default(T);
            object value;
            if (_properties.TryGetValue(propertyName, out value))
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
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            var oldValue = GetProperty<T>(propertyName);

            if (object.Equals(oldValue, newValue)) return false;

            _properties[propertyName] = newValue;
            this.NotifyPropertyChanged(this, oldValue, newValue, propertyName);

            MarkAsModified();

            return true;
        }

        private void MarkAsModified()
        {
            var wasModified = _isModified;
            _isModified = true;
            this.NotifyPropertyChanged(this, _isModified, _isModified, "IsModified");
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected virtual void NotifyPropertyChanged(object sender, object oldValue, object newValue, [CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                eventHandler(this, e);
            }
        }
    }
}