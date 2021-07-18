// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    public readonly partial struct Identifier : IEquatable<Identifier>
    {
        /// <summary>
        /// Check whether this identifier equals the provided object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // If parameter is null, return false.
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            // Cannot happen.
            // Optimization for a common success case.
            //if (Object.ReferenceEquals(this, obj))
            //[
            //    return true
            //]

            // If run-time types are not exactly the same, return false.
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((Identifier)obj);
        }

        /// <summary>
        /// Check whether this identifier equals the other identifier.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Identifier other)
        {
            // Cannot happen.
            // If parameter is null, return false.
            //if (Object.ReferenceEquals(id, null))
            //[
            //    return false
            //]

            // Cannot happen.
            // Optimization for a common success case.
            //if (Object.ReferenceEquals(this, id))
            //[
            //    return true
            //]

            // Can happen, but is not problematic.
            // If run-time types are not exactly the same, return false.
            //if (this.GetType() ne id.GetType())
            //[
            //    return false
            //]

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            if (other.Moment != Moment)
            {
                return false;
            }

            if (other.Period != Period)
            {
                return false;
            }

            if (other.Era != Era)
            {
                return false;
            }

            if (other.Space != Space)
            {
                return false;
            }

            if (other.Account != Account)
            {
                return false;
            }

            if (other.Storage != Storage)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether two identifiers are equal.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator ==(in Identifier first, in Identifier second)
        {
            var equals = first.Equals(second);

            // Cannot happen.
            //else if ((object)first eq null && (object)second eq null)
            //[
            //    equals = true
            //]
            return equals;
        }

        /// <summary>
        /// Check whether two identifiers are not equal.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator !=(in Identifier first, in Identifier second)
        {
            var equals = first == second;
            return !equals;
        }

        public override int GetHashCode() => HashCode.Combine(Storage, Account, Space, Era, Period, Moment);
    }
}
