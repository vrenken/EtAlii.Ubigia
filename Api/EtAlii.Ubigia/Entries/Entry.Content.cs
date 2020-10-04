namespace EtAlii.Ubigia
{
    using System;

    public sealed partial class Entry
    {
        public string Type => ((IComponentEditableEntry)this).TypeComponent.Type;

        string IEditableEntry.Type
        {
            get => ((IComponentEditableEntry)this).TypeComponent.Type;
            set
            {
                if (!String.IsNullOrEmpty(((IComponentEditableEntry)this).TypeComponent.Type))
                {
                    throw new InvalidOperationException("Unable to set Entry.Type. This property has already been assigned");
                }
                ((IComponentEditableEntry)this).TypeComponent = new TypeComponent { Type = value };
            }
        }

        TypeComponent IComponentEditableEntry.TypeComponent { get; set; }


        public string Tag => ((IComponentEditableEntry)this).TagComponent.Tag;

        string IEditableEntry.Tag
        {
            get => ((IComponentEditableEntry)this).TagComponent.Tag;
            set
            {
                if (!String.IsNullOrEmpty(((IComponentEditableEntry)this).TagComponent.Tag))
                {
                    throw new InvalidOperationException("Unable to set Entry.Tag. This property has already been assigned");
                }
                ((IComponentEditableEntry)this).TagComponent = new TagComponent { Tag = value };
            }
        }

        TagComponent IComponentEditableEntry.TagComponent { get; set; }
}
}
