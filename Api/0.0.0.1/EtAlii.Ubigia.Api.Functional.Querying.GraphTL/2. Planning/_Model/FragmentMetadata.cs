namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.ObjectModel;

    internal sealed class FragmentMetadata
    {
        public ObservableCollection<Structure> Items { get; } = new ObservableCollection<Structure>();
        
        public ReadOnlyObservableCollection<FragmentMetadata> Children { get; }
        private readonly ObservableCollection<FragmentMetadata> _editableChildren;

        public FragmentMetadata Parent { get; }

        public Fragment Source { get; }
        
        public FragmentMetadata(
            Fragment source,
            FragmentMetadata parent) 
        {
            Source = source;
            Parent = parent;
            
            _editableChildren = new ObservableCollection<FragmentMetadata>();
            Children = new ReadOnlyObservableCollection<FragmentMetadata>(_editableChildren);
        }
        
        internal void AddChildFragments(FragmentMetadata[] children)
        {
            foreach (var child in children)
            {
                _editableChildren.Add(child);
            }
        }

        public override string ToString()
        {
            return Source.ToString();
        }
    }
}
