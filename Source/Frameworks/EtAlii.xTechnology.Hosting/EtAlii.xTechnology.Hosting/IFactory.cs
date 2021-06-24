// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public interface IFactory<out TInstance>
    {
        TInstance Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails);
    }
}
