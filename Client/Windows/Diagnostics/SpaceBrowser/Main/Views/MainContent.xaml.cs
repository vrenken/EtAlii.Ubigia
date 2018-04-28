namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Linq;
    using System.Windows.Controls;
    using Xceed.Wpf.AvalonDock.Layout;
    using Xceed.Wpf.AvalonDock.Layout.Serialization;

    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    public partial class MainContent : UserControl
    {
        public MainContent()
        {
                InitializeComponent();
        }

//
//        private void SaveLayoutSettings()
//        {
//            using (var stream = new MemoryStream())
//            {
//                var serializer = new XmlLayoutSerializer(DockingManager);
//                serializer.Serialize(stream);
//                stream.Seek(0, SeekOrigin.Begin);
//
//                using (var reader = new StreamReader(stream))
//                {
//                    Settings.Default.MainWindowLayout = reader.ReadToEnd();
//                }
//            }
//        }
//
//        private void LoadLayoutSettings()
//        {
//            if (!String.IsNullOrWhiteSpace(Settings.Default.MainWindowLayout))
//            {
//                var currentLayout = DockingManager.Layout.Children.OfType<LayoutContent>()
//                                                                  .Where(c => c.ContentId != null)
//                                                                  .ToArray();
//                var bytes = Encoding.Default.GetBytes(Settings.Default.MainWindowLayout);
//                using (var stream = new MemoryStream(bytes))
//                {
//                    var serializer = new XmlLayoutSerializer(DockingManager);
//                    serializer.LayoutSerializationCallback += (sender, e) =>
//                    {
//                        ApplyLayout(currentLayout, e);
//                    };
//                    serializer.Deserialize(stream);
//                }
//            }
//        }

        private void ApplyLayout(LayoutContent[] currentLayout, LayoutSerializationCallbackEventArgs e)
        {
            var layoutContent = currentLayout.FirstOrDefault(c => c.ContentId == e.Model.ContentId);
            if (layoutContent != null)
            {
                e.Content = layoutContent.Content;
            }
        }

    }
}
