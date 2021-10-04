// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;

    public class ServiceFactory
    {
        public IService Create(ServiceConfiguration serviceConfiguration, IHost host)
        {
            var factoryTypeName = serviceConfiguration.Factory;

            if (string.IsNullOrEmpty(factoryTypeName))
            {
                throw new InvalidOperationException($"Configuration section '{serviceConfiguration.Section.Path}' has no factory defined.");
            }

            var type = Type.GetType(factoryTypeName, false);
            if (type == null)
            {
                throw new InvalidOperationException($"Unable to instantiate factory: {factoryTypeName}");
            }

            if (!(Activator.CreateInstance(type) is IServiceFactory factory))
            {
                throw new InvalidOperationException($"Unable to activate factory: {factoryTypeName}");
            }

            var status = new Status(serviceConfiguration.Section.Key) { Summary = "Unknown", Title = serviceConfiguration.Section.Key };
            return factory.Create(serviceConfiguration, status, host);
        }
    }
}
