namespace EtAlii.xTechnology.Enumerations
{
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class EnumDelta<T>
    {
        public EnumDelta(IEnumerable<T> added, IEnumerable<T> removed)
        {
            Added = new List<T>(added);
            Removed = new List<T>(removed);
        }

        public List<T> Added { get; }

        public List<T> Removed { get; }

        public override string ToString()
        {
            return $"Added: {ToCommaSeperatedList(Added)} - Removed: {ToCommaSeperatedList(Removed)}";
        }


        private string ToCommaSeperatedList<T>(IEnumerable<T> enumerable)
        {
            var list = enumerable.Select(item => item.ToString());
            return String.Join(", ", list);
        }

    }
}
