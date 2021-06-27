// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public partial class DynamicNode : IEquatable<DynamicNode>
    {
        public override bool Equals(object obj)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((DynamicNode)obj);
        }

        public bool Equals(DynamicNode other)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (GetType() != other.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            if (_entry.Id != ((IInternalNode)other).Entry.Id)
            {
                return false;
            }

            return true;
        }

        public static bool operator ==(DynamicNode first, DynamicNode second)
        {
            var equals = false;
            if ((object)first != null && (object)second != null)
            {
                equals = first.Equals(second);
            }
            else if ((object)first == null && (object)second == null)
            {
                equals = true;
            }

            return equals;
        }

        public static bool operator !=(DynamicNode first, DynamicNode second)
        {
            var equals = first == second;
            return !equals;
        }

        // Is this GetHashCode behavior correct?
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/75
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _entry.Id.GetHashCode();
        }
    }
}
