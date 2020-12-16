namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.ComponentModel;
    using System.Dynamic;

    public partial class DynamicNode
    {
        private PropertyDictionary _properties = new();

        PropertyDictionary IInternalNode.GetProperties()
        {
            return _properties;
        }

        //void IInternalNode.SetProperties(IPropertiesDictionary properties)
        //[
        //    if [properties = = null]
        //    [
        //        throw new ArgumentNullException("properties")
        //    ]
        //    _properties = properties
        //]
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
            if (_properties.TryGetValue(binder.Name, out var existingValue))
            {
                _properties[binder.Name] = value;
                if (value == existingValue)
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

//        private void MarkAsModified()
//        [
//            //var wasModified = _isModified
//            _isModified = true
//            NotifyPropertyChanged(nameof(INode.IsModified)) // this, _isModified, _isModified,
//        ]

        #region Not supported actions.

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            throw new NotSupportedException(_notSupportedErrorMessage);
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            throw new NotSupportedException(_notSupportedErrorMessage);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            throw new NotSupportedException(_notSupportedErrorMessage);
        }

        #endregion Not supported actions.

    }
}
