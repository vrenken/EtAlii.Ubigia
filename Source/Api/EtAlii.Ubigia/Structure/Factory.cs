// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using EtAlii.xTechnology.MicroContainer;

    public static class Factory
    {
        public static TInstance Create<TInstance, TExtension>(IExtensible options)
            where TExtension : IExtension
        {
            var container = new Container();

            foreach (var extension in options.GetExtensions<TExtension>())
            {
                extension.Initialize(container);
            }

            return container.GetInstance<TInstance>();
        }

        public static (TFirstInstance, TSecondInstance) Create<TFirstInstance, TSecondInstance, TExtension>(IExtensible options)
            where TExtension : IExtension
        {
            var container = new Container();

            foreach (var extension in options.GetExtensions<TExtension>())
            {
                extension.Initialize(container);
            }

            return (container.GetInstance<TFirstInstance>(), container.GetInstance<TSecondInstance>());
        }
    }
}
