namespace EtAlii.Servus.Provisioning.Hosting
{
    using System.CodeDom;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Interaction logic for TaskbarIcon.xaml
    /// </summary>
    public partial class TaskbarIcon : Hardcodet.Wpf.TaskbarNotification.TaskbarIcon , ITaskbarIcon
    {
        public TaskbarIcon()
        {
            InitializeComponent();
        }
    }
}
