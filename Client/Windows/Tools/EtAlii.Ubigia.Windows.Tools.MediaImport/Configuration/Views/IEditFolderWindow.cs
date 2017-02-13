namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System.Windows;
    using System.Windows.Forms.VisualStyles;

    internal interface IEditFolderWindow
    {
        object DataContext { get; set; }

        Window Owner { get; set; }

        bool? ShowDialog();

        void InitializeComponent();
    }
}