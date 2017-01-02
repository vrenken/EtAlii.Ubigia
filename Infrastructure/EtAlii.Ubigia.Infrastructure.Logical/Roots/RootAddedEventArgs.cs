namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public class RootAddedEventArgs : EventArgs
    {
        public Guid SpaceId { get; }
        public Root Root { get; }

        public RootAddedEventArgs(Root root, Guid spaceId)
        {
            SpaceId = spaceId;
            Root = root;
        }
    }
}