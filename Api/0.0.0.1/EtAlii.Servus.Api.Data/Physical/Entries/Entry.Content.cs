namespace EtAlii.Servus.Api
{
    using System;

    public sealed partial class Entry
    {
        public string Type { get { return _type.Type; } }
        private TypeComponent _type;

        string IEditableEntry.Type
        {
            get
            {
                return _type.Type;
            }
            set
            {
                if (!String.IsNullOrEmpty(_type.Type))
                {
                    throw new InvalidOperationException("Unable to set Entry.Type. This property has already been assigned");
                }
                _type = new TypeComponent(value);
            }
        }

        TypeComponent IComponentEditableEntry.TypeComponent
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
    }
}
