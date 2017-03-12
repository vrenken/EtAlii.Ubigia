namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;

    public class SpaceAddedEventArgs : EventArgs
    {
        public SpaceAddedEventArgs(Space space, SpaceTemplate template)
        {
            Space = space;
            Template = template;
        }

        public Space Space { get; }
        public SpaceTemplate Template { get; }
    }
}