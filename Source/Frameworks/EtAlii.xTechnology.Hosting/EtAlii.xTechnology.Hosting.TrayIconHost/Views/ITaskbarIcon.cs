// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Drawing;
    using System.Windows;
    using System.Windows.Threading;

    public interface ITaskbarIcon
    {
        Icon Icon { get; set; }
        object DataContext { get; set; }
        Dispatcher Dispatcher { get; }
        Visibility Visibility { get; set; }
    }
}