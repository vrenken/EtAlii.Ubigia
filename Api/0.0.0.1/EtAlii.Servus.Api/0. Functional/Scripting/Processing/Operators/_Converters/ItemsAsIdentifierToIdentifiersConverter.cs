namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Structure;

    internal class ItemsAsIdentifierToIdentifiersConverter : IItemsToIdentifiersConverter
    {
        public Identifier[] Convert(object item)
        {
            return new Identifier[] { (Identifier)item };
        }
    }
}
