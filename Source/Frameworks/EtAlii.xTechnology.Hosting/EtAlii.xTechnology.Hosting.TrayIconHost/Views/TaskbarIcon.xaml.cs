// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
