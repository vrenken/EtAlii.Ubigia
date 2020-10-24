namespace EtAlii.Ubigia.Windows
{
    using System;
    using System.Globalization;
    using System.Windows;

    public sealed class StringToVisibilityConverter : BooleanConverter<Visibility>
    {
        public StringToVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed) { }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return base.Convert(value is string, targetType, parameter, culture);
        }
    }
}
