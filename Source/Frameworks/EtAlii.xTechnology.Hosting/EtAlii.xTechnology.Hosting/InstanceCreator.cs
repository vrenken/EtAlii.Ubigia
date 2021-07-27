// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Create new instances for the specified configuration.
    /// This class expects a factory definition with the corresponding namespace / assembly.
    /// </summary>
    public class InstanceCreator : IInstanceCreator
    {
        /// <inheritdoc />
        public bool TryCreate<TInstance>(IConfigurationSection configuration, IConfiguration configurationRoot, IConfigurationDetails configurationDetails, string name, out TInstance instance) => TryCreate(configuration, configurationRoot, configurationDetails, name, out instance, false);

        /// <inheritdoc />
        public bool TryCreate<TInstance>(IConfigurationSection configuration, IConfiguration configurationRoot, IConfigurationDetails configurationDetails, string name, out TInstance instance, bool throwOnNoFactory)
        {
            var factoryTypeName = configuration.GetValue<string>("Factory");

            if (throwOnNoFactory && factoryTypeName == null)
            {
                name = string.Join(char.ToUpper(name[0]), name.Skip(1));
                throw new InvalidOperationException($"{name} without factory defined in configuration.");
            }

            if (!string.IsNullOrEmpty(factoryTypeName))
            {
                // We want to assist with being able to provide the namespace and assembly separately.
                // This should result in shorter factory entries.
                var @namespace = configuration.GetValue<string>("Namespace");
                if (!string.IsNullOrEmpty(@namespace))
                {
                    factoryTypeName = $"{@namespace}.{factoryTypeName}";
                }
                var assembly = configuration.GetValue<string>("Assembly");
                if (!string.IsNullOrEmpty(assembly))
                {
                    factoryTypeName = $"{factoryTypeName}, {assembly}";
                }

                var type = Type.GetType(factoryTypeName, false);
                if (type == null)
                {
                    throw new InvalidOperationException($"Unable to instantiate {name} factory: {factoryTypeName}");
                }

                if (!(Activator.CreateInstance(type) is IFactory<TInstance> factory))
                {
                    throw new InvalidOperationException($"Unable to activate {name} factory: {factoryTypeName}");
                }

                instance = factory.Create(configuration, configurationRoot, configurationDetails);
                return true;
            }
            instance = default;
            return false;
        }
    }
}
