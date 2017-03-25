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
            return $"Added: {Added.ToCommaSeperatedList()} - Removed: {Removed.ToCommaSeperatedList()}";
        }
    }
}
