// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.ComponentModel;
    using System.Dynamic;

    public partial class DynamicNode
    {
        private readonly PropertyDictionary _properties;

        PropertyDictionary IInternalNode.GetProperties()
        {
            return _properties;
        }

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
