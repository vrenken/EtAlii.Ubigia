namespace EtAlii.xTechnology.Enumerations
{
    using EtAlii.xTechnology.Collections;
    using System.Collections.Generic;

    public class EnumDelta<T>
    {
        public EnumDelta(IEnumerable<T> added, IEnumerable<T> removed)
        {
            Added = new List<T>(added);
            Removed = new List<T>(removed);
        }

        public List<T> Added { get; private set; }

        public List<T> Removed { get; private set; }

        public override string ToString()
        {
            return string.Format("Added: {0} - Removed: {1}", Added.ToCommaSeperatedList(), Removed.ToCommaSeperatedList());
        }
    }
}
