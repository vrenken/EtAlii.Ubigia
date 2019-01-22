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
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = new StringBuilder();

            switch (value)
            {
                case Identifier identifier:
                    return identifier.ToTimeString();
                case IEnumerable<Identifier> identifiers:
                    foreach (var id in identifiers)
                    {
                        var itemRepresentation = Convert(id, targetType, parameter, culture);
                        result.Append(itemRepresentation);
                        result.AppendLine();
                    }
                    break;
                case DynamicNode dn:
                    var node = (IInternalNode)dn;
                    var properties = node.GetProperties();
                    foreach (var kvp in properties)
                    {
                        result.AppendFormat("{0}: {1}", kvp.Key, kvp.Value);
                        result.AppendLine();
                    }
                    break;
                case IEnumerable<DynamicNode> dynamicNodes:
                    foreach (var dynamicNode in dynamicNodes)
                    {
                        var itemRepresentation = Convert(dynamicNode, targetType, parameter, culture);
                        result.Append(itemRepresentation);
                        result.AppendLine();
                    }
                    break;
                case IPropertyDictionary propertyDictionary:
                    foreach (var kvp in propertyDictionary)
                    {
                        result.AppendFormat("{0}: {1}", kvp.Key, kvp.Value);
                        result.AppendLine();
                    }
                    break;
                case IEnumerable<IPropertyDictionary> propertyDictionaries:
                    foreach (var pd in propertyDictionaries)
                    {
                        var itemRepresentation = Convert(pd, targetType, parameter, culture);
                        result.Append(itemRepresentation);
                        result.AppendLine();
                    }
                    break;
                case int i:
                    result.Append(i.ToString());
                    break;
                case DateTime dateTime:
                    result.Append(dateTime.ToString());
                    break;
                case string s:
                    result.Append(s);
                    break;
                default:
                    result.Append("Unknown");
                    break;
            }

            return result.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
