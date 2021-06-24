// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System;

    public partial struct ContainerIdentifier : IEquatable<ContainerIdentifier>
    {
        public override bool Equals(object obj)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            // If run-time types are not exactly the same, return false.
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((ContainerIdentifier)obj);
        }

        public bool Equals(ContainerIdentifier other)
        {
            // Cannot happen: If parameter is null, return false.
            // Cannot happen: Optimization for a common success case.
            // Can happen, but is not problematic: If run-time types are not exactly the same, return false.

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            if (other.Paths.Length != Paths.Length)
            {
                return false;
            }

            for (var i = 0; i < Paths.Length; i++)
            {
                if (other.Paths[i] != Paths[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator ==(ContainerIdentifier first, ContainerIdentifier second)
        {
            var equals = first.Equals(second);
            return equals;
        }

        public static bool operator !=(ContainerIdentifier first, ContainerIdentifier second)
        {
            var equals = first == second;
            return !equals;
        }

        public override int GetHashCode() => HashCode.Combine(Paths);
    }
}
