namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Windows;

    internal interface IEditFolderWindow
    {
        object DataContext { get; set; }

        Window Owner { get; set; }

        bool? ShowDialog();

        void InitializeComponent();
    }
}