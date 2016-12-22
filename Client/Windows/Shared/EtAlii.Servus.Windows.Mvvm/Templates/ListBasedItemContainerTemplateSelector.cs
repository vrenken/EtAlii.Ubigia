namespace EtAlii.Servus.Windows.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public class ListBasedItemContainerTemplateSelector : ItemContainerTemplateSelector
    {
        public ListBasedItemContainerTemplateSelector()
        {
            Templates = new List<DataTemplate>();
        }

        public List<DataTemplate> Templates { get; set; }

        public override DataTemplate SelectTemplate(object item, ItemsControl parentItemsControl)
        {
            var match = Templates.Where(t => (t.DataType as Type).IsAssignableFrom(item.GetType())).FirstOrDefault();
            if (match != null)
            {
                return match;
            }
            return base.SelectTemplate(item, parentItemsControl);
        }
    }

}
