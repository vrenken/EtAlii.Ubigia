namespace EtAlii.xTechnology.Hosting
{
    /// <summary>
    /// Interaction logic for TaskbarIcon.xaml
    /// </summary>
    public partial class TaskbarIcon : ITaskbarIcon
    {
        public TaskbarIcon(ITaskbarIconViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
