﻿namespace EtAlii.Ubigia.Persistence
{
    using System;

    internal class BlobStorer : IBlobStorer
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IImmutableFolderManager _folderManager;

        public BlobStorer(IImmutableFolderManager folderManager, 
                          IPathBuilder pathBuilder)
        {
            _folderManager = folderManager;
            _pathBuilder = pathBuilder;
        }
        
        public void Store(ContainerIdentifier container, Blob blob)
        {
            var blobName = Blob.GetName(blob);
            container = ContainerIdentifier.Combine(container, blobName);
            var folder = _pathBuilder.GetFolder(container);

            _folderManager.Create(folder);

            Blob.SetStored(blob, false);
            Blob.SetSummary(blob, null);
            _folderManager.SaveToFolder(blob, "Blob", folder);
            Blob.SetStored(blob, true);
            
            var summary = new BlobSummary 
            {
                IsComplete = false, 
                TotalParts = blob.TotalParts, 
                AvailableParts = Array.Empty<ulong>()
            };

            Blob.SetSummary(blob, summary);
        }
    }
}
