﻿namespace EtAlii.Servus.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

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