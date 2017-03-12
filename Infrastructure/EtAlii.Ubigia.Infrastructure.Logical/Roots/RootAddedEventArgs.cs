namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;

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