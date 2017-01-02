namespace EtAlii.Servus.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ContentComponent : CompositeComponent
    {
        public ContentComponent()
        { 
        }

        internal override string Name { get { return _name; } }
        private const string _name = "Content";

        public override void Apply(Client.Model.IEditableEntry entry)
        {
            // TODO
        } 
    }
}
