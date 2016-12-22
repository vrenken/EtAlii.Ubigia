namespace EtAlii.Servus.Windows.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;

    public class ListBasedStyleTemplateSelector : StyleSelector
    {
        public ListBasedStyleTemplateSelector()
        {
            Templates = new List<StyleTemplate>();
        }

        public List<StyleTemplate> Templates { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            var match = Templates.Where(t => (t.DataType as Type).IsAssignableFrom(item.GetType())).FirstOrDefault();
            if (match != null) return match.Style;
            return base.SelectStyle(item, container);
        }
    }

}
