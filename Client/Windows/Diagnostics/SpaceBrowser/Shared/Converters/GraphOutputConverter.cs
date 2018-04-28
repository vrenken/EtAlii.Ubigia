namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Data;
    using EtAlii.Ubigia.Api.Logical;

    public class GraphOutputConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Identifier)
            {
                return ((Identifier)value).ToTimeString();
            }
            else if (value is IEnumerable<Identifier>)
            {
                var result = new StringBuilder();
                foreach (var v in (IEnumerable<Identifier>)value)
                {
                    var itemRepresentation = Convert(v, targetType, parameter, culture);
                    result.Append(itemRepresentation);
                    result.AppendLine();
                }
                return result.ToString();
            }
            else if (value is DynamicNode)
            {
                var node = (IInternalNode) value;
                var properties = node.GetProperties();
                var result = new StringBuilder();
                foreach (var kvp in properties)
                {
                    result.AppendFormat("{0}: {1}", kvp.Key, kvp.Value);
                    result.AppendLine();
                }
                return result.ToString();
            }
            else if (value is IEnumerable<DynamicNode>)
            {
                var result = new StringBuilder();
                foreach (var v in (IEnumerable<DynamicNode>)value)
                {
                    var itemRepresentation = Convert(v, targetType, parameter, culture);
                    result.Append(itemRepresentation);
                    result.AppendLine();
                }
                return result.ToString();
            }
            else if (value is IPropertyDictionary)
            {
                var result = new StringBuilder();
                foreach (var kvp in (IPropertyDictionary)value)
                {
                    result.AppendFormat("{0}: {1}", kvp.Key, kvp.Value);
                    result.AppendLine();
                }
                return result.ToString();
            }
            else if (value is IEnumerable<IPropertyDictionary>)
            {
                var result = new StringBuilder();
                foreach (var v in (IEnumerable<IPropertyDictionary>)value)
                {
                    var itemRepresentation = Convert(v, targetType, parameter, culture);
                    result.Append(itemRepresentation);
                    result.AppendLine();
                }
                return result.ToString();
            }
            else if (value is int)
            {
                return ((int) value).ToString();
            }
            else if (value is DateTime)
            {
                return ((DateTime) value).ToString();
            }
            else if (value is string)
            {
                return (string) value;
            }
            else
            {
                return "Unknown";
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
