namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.ObjectModel;

    internal sealed class FragmentMetadata
    {
        public ObservableCollection<Structure> Items { get; } = new();

        public FragmentMetadata[] Children { get; }

        public FragmentMetadata Parent { get; private set; }

        public Fragment Source { get; }

        public FragmentMetadata(
            Fragment source,
            FragmentMetadata[] children)
        {
            Source = source;

            Children = children;
            foreach (var child in children)
            {
                child.Parent = this;
            }
        }

        public override string ToString()
        {
            return Source.ToString();
        }
    }
}
