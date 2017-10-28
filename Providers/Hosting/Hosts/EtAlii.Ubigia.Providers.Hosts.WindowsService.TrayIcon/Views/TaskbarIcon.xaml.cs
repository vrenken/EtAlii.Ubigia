namespace EtAlii.Ubigia.Provisioning.Hosting
{
    /// <summary>
    /// Interaction logic for TaskbarIcon.xaml
    /// </summary>
    public partial class TaskbarIcon : Hardcodet.Wpf.TaskbarNotification.TaskbarIcon , ITaskbarIcon
    {
        public TaskbarIcon(ITaskbarIconViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
