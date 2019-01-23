namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;

    public class ChangeTracker : IChangeTracker
    {
        public bool HasChanges { get; }

        public IEnumerable<NodeTrackingReference> Entries => _entries;
        private readonly List<NodeTrackingReference> _entries;

        public ChangeTracker()
        {
            _entries = new List<NodeTrackingReference>();
            HasChanges = false;
        }

        public void Dispose()
        {
            // Dispose the ChangeTracker.
        }
    }
}