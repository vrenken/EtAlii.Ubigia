using System.Windows;

namespace Samples.Tutorials.ContextMenus
{
  /// <summary>
  /// Interaction logic for InlineContextMenuWindow.xaml
  /// </summary>
  public partial class InlineContextMenuWindow : Window
  {
    public InlineContextMenuWindow()
    {
      InitializeComponent();
    }


    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
      //clean up notifyicon (would otherwise stay open until application finishes)
      MyNotifyIcon.Dispose();

      base.OnClosing(e);
    }
  }
}
