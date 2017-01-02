namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.Structure;

    internal class ItemsAsNodeToIdentifiersConverter : IItemsToIdentifiersConverter
    {
        public Identifier[] Convert(object item)
        {
            return new Identifier[] {((INode) item).Id};
        }
    }
}
