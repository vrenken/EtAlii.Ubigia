namespace EtAlii.Servus.Windows.Mvvm
{
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Style")]
    public class StyleTemplate
    {
        public object DataType { get; set; }
        public Style Style { get; set; }
    }
}
