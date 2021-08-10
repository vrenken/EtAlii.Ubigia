// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public abstract class ServiceFactoryBase<TService> : ServiceFactoryBase
        where TService : class, IService
    {
        /// <inheritdoc />
        public override IService Create(
            IConfigurationSection configuration,
            IConfigurationRoot configurationRoot,
            IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IService, TService>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IService>();
        }
    }
}
