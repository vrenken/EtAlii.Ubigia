// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public interface IInstanceCreator
    {
        bool TryCreate<TInstance>(IConfigurationSection configuration, IConfigurationDetails configurationDetails, string name, out TInstance instance);
        bool TryCreate<TInstance>(IConfigurationSection configuration, IConfigurationDetails configurationDetails, string name, out TInstance instance, bool throwOnNoFactory);
    }
}