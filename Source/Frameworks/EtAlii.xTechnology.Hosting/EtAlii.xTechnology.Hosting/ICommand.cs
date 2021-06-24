// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;

    public interface ICommand
    {
        string Name { get; }
        void Execute();
        bool CanExecute { get; }

        event EventHandler CanExecuteChanged;
    }
}
