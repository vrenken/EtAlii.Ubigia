namespace EtAlii.Ubigia.Windows.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public class ListBasedTemplateSelector : DataTemplateSelector
    {
        public ListBasedTemplateSelector()
        {
            Templates = new List<DataTemplate>();
        }

        public List<DataTemplate> Templates { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var match = Templates.FirstOrDefault(t => (t.DataType as Type).IsInstanceOfType(item));
            if (match != null)
            {
                return match;
            }
            return base.SelectTemplate(item, container);
        }
    }

}
