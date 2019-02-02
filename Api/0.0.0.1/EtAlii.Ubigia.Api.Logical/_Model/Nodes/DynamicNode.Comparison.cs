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
            bool equals = false;
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
            bool equals = first == second;
            return !equals;
        }

        #region Hashing

        public override int GetHashCode() 
        {
            // Using the hash code of the original entry ID the Node is created with is valid:
            // https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netframework-4.7.2
            // Below a sentence to strengthen this argument.
            // - If two objects compare as equal, the GetHashCode() method for each object must return the same value.
            // However, if two objects do not compare as equal, the GetHashCode() methods for the two objects do not
            // have to return different values.
            return _uniqueHashCode;
        }

        #endregion Hashing
    }
}