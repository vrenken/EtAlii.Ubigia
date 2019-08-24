namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Collections.Generic;

    public class ChangeTracker : IChangeTracker
    {
        public bool HasChanges { get; }

        public IEnumerable<NodeTrackingReference> Entries => _entries;
        private readonly NodeTrackingReference[] _entries;

        public ChangeTracker()
        {
            //_entries = new List<NodeTrackingReference>()
            _entries = Array.Empty<NodeTrackingReference>();
            HasChanges = false;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Dispose any resources that need to be get rid of.
        }

        ~ChangeTracker()
        {
            Dispose(false);
        }
    }
}