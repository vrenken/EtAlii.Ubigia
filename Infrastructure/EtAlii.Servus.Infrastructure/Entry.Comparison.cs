namespace EtAlii.Servus.Model.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class Entry
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
            bool equals = true;

            var entry = (Entry)obj;
            if (entry.Id != Id)
            {
                equals = false;
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion Hashing

    }
}
