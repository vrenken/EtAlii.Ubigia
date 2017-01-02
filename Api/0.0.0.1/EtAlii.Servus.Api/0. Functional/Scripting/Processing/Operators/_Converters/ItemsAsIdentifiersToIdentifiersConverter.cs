﻿namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Structure;

    internal class ItemsAsIdentifiersToIdentifiersConverter : IItemsToIdentifiersConverter
    {
        public Identifier[] Convert(object items)
        {
            return ((IEnumerable<Identifier>) items).Select(item => item).ToArray();
        }
    }
}
