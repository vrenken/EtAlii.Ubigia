namespace EtAlii.xTechnology.Enumerations
{
    using EtAlii.xTechnology.Collections;
    using System.Collections.Generic;

    public class EnumDelta<T>
    {
        private List<T> _added;
        private List<T> _removed;

        public EnumDelta(IEnumerable<T> added, IEnumerable<T> removed)
        {
            Added = new List<T>(added);
            Removed = new List<T>(removed);
        }

        public List<T> Added
        {
            get { return _added; }
            private set { _added = value; }
        }

        public List<T> Removed
        {
            get { return _removed; }
            private set { _removed = value; }
        }

        public override string ToString()
        {
            return string.Format("Added: {0} - Removed: {1}", Added.ToCommaSeperatedList(), Removed.ToCommaSeperatedList());
        }
    }
}
