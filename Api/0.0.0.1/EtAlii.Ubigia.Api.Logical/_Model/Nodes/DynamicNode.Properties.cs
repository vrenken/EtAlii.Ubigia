namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Runtime.CompilerServices;

    public partial class DynamicNode
    {
        private PropertyDictionary _properties = new PropertyDictionary();

        PropertyDictionary IInternalNode.GetProperties()
        {
            return _properties;
        }

        //void IInternalNode.SetProperties(IPropertiesDictionary properties)
        //{
        //    if (properties == null)
        //    {
        //        throw new ArgumentNullException("properties");
        //    }
        //    _properties = properties;
        //}

        public bool TryGetValue(string key, out object value)
        {
            return _properties.TryGetValue(key, out value);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _properties.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _properties[binder.Name] = value;
            return true;
        }

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="newValue">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        private bool SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, newValue)) return false;

            var oldValue = storage;
            storage = newValue;
            NotifyPropertyChanged(this, storage, newValue, propertyName);

            MarkAsModified();

            return true;
        }

        private void MarkAsModified()
        {
            var wasModified = _isModified;
            _isModified = true;
            NotifyPropertyChanged(this, _isModified, _isModified, nameof(INode.IsModified));
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        private void NotifyPropertyChanged(object sender, object oldValue, object newValue, [CallerMemberName] string propertyName = null)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                eventHandler(this, e);
            }
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