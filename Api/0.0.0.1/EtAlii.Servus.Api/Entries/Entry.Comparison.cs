namespace EtAlii.Servus.Api
{
    public sealed partial class Entry
    {
        public static bool operator ==(Entry first, Entry second)
        {
            bool equals = false;
            if ((object)first != null && (object)second != null)
            {
                equals = first.Id.Equals(second.Id);
            }
            else if ((object)first == null && (object)second == null)
            {
                equals = true;
            }
            return equals;
        }

        public static bool operator !=(Entry first, Entry second)
        {
            bool equals = first == second;
            return !equals;
        }

        #region Hashing

        public override bool Equals(object obj)
        {
            return Equals(obj as Entry);
        }

        public bool Equals(Entry entry)
        {
            // If parameter is null, return false. 
            if (ReferenceEquals(entry, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (ReferenceEquals(this, entry))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            //if (this.GetType() != entry.GetType())
            //{
            //    return false;
            //}

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            return Id == entry.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion Hashing

    }
}
