﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    internal interface IHostCommandsConverter
    {
        MenuItemViewModel[] ToViewModels(ICommand[] commands);
    }
}