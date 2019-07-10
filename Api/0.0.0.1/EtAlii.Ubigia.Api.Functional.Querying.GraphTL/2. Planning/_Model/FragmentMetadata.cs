namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.ObjectModel;

    internal sealed class FragmentMetadata
    {
        public ObservableCollection<Structure> Items { get; } = new ObservableCollection<Structure>();

        public FragmentMetadata[] Children { get; private set; }

        public FragmentMetadata Parent { get; }

        public Fragment Source { get; }
        
        public FragmentMetadata(
            Fragment source,
            FragmentMetadata parent) 
        {
            Source = source;
            Parent = parent;
        }
        
        internal static void SetChildFragments(FragmentMetadata fragmentMetadata, FragmentMetadata[] children)
        {
            fragmentMetadata.Children = children;
        }

        public override string ToString()
        {
            return Source.ToString();
        }
    }
}
