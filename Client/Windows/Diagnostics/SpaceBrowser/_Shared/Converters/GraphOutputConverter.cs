namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Data;
    using EtAlii.Ubigia.Api.Logical;

    public class GraphOutputConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case Identifier identifier: return identifier.ToTimeString();
                case IEnumerable<Identifier> identifiers:
                {
                    var result = new StringBuilder();
                    foreach (var v in identifiers)
                    {
                        var itemRepresentation = Convert(v, targetType, parameter, culture);
                        result.Append(itemRepresentation);
                        result.AppendLine();
                    }
                    return result.ToString();
                }
                case IInternalNode node:
                {
                    var properties = node.GetProperties();
                    var result = new StringBuilder();
                    foreach (var kvp in properties)
                    {
                        result.AppendFormat("{0}: {1}", kvp.Key, kvp.Value);
                        result.AppendLine();
                    }
                    return result.ToString();
                }
                case IEnumerable<DynamicNode> nodes:
                {
                    var result = new StringBuilder();
                    foreach (var node in nodes)
                    {
                        var itemRepresentation = Convert(node, targetType, parameter, culture);
                        result.Append(itemRepresentation);
                        result.AppendLine();
                    }
                    return result.ToString();
                }
                case IPropertyDictionary propertyDictionary:
                {
                    var result = new StringBuilder();
                    foreach (var kvp in propertyDictionary)
                    {
                        result.AppendFormat("{0}: {1}", kvp.Key, kvp.Value);
                        result.AppendLine();
                    }
                    return result.ToString();
                }
                case IEnumerable<IPropertyDictionary> propertyDictionaries:
                {
                    var result = new StringBuilder();
                    foreach (var propertyDictionary in propertyDictionaries)
                    {
                        var itemRepresentation = Convert(propertyDictionary, targetType, parameter, culture);
                        result.Append(itemRepresentation);
                        result.AppendLine();
                    }
                    return result.ToString();
                }
                case int i: return i.ToString();
                case DateTime dateTime: return dateTime.ToString(culture);
                case string s: return s;
                default: return "Unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
