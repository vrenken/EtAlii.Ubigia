// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public abstract class ModuleFactoryBase<TModule> : ModuleFactoryBase
        where TModule: IModule
    {
        /// <inheritdoc />
        public override IModule Create(IConfigurationSection configuration, IConfigurationRoot configurationRoot, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IModule, TModule>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IModule>();
        }
    }
}
