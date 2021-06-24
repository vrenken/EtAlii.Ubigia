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

        #region Hashing

        #pragma warning disable S2328
        // TODO: Investigate Node.GetHashCode behavior.
        // Ok, calculating the hash from a non-readonly member is a bad thing, however in the case of a Node we use
        // a pattern of lazy-loading/updating for which it feels it is allowed to calculate the hash from the most
        // recent Identifier. However, we must investigate this further to see if it really is not a problem.
        // Thinking about it further it really might be a bad thing. However it is outside of the current scope
        // of activities (providing proof for Ubiquitous Information Systems). Therefore we convert the
        // SonarCube bug warning into a to-do. Below some more information:
        // http://sonarqube/coding_rules?open=csharpsquid%3AS2328&rule_key=csharpsquid%3AS2328
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _entry.Id.GetHashCode();
        }
        #pragma warning restore S2328

        #endregion Hashing
    }
}
