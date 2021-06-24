// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    public sealed partial class ContentDefinition
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

            return Equals((ContentDefinition)obj);
        }

        public bool Equals(ContentDefinition other)
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
            //if (this.GetType() ne contentDefinition.GetType())
            //[
            //    return false
            //]

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            if (other.Checksum != Checksum)
            {
                return false;
            }

            if (other.Parts.Length != Parts.Length)
            {
                return false;
            }

            if (other.Size != Size)
            {
                return false;
            }

            return true;
        }

        public static bool operator ==(ContentDefinition first, ContentDefinition second)
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

        public static bool operator !=(ContentDefinition first, ContentDefinition second)
        {
            var equals = first == second;
            return !equals;
        }

        public override int GetHashCode() => HashCode.Combine(Size, Checksum, Parts);

    }
}
