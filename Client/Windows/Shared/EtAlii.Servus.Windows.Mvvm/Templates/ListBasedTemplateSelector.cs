namespace EtAlii.Servus.Windows.Mvvm
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

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var match = Templates.Where(t => (t.DataType as Type).IsAssignableFrom(item.GetType())).FirstOrDefault();
            if (match != null)
            {
                return match;
            }
            return base.SelectTemplate(item, container);
        }
    }

}
