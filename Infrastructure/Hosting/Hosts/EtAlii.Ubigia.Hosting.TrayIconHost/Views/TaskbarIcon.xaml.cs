namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    /// <summary>
    /// Interaction logic for TaskbarIcon.xaml
    /// </summary>
    public partial class TaskbarIcon : Hardcodet.Wpf.TaskbarNotification.TaskbarIcon, ITaskbarIcon
    {
        public TaskbarIcon(ITaskbarIconViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
