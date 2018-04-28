namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Mvvm;

    public class TextualError : BindableBase
    {
        public string Text { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
    }
}
