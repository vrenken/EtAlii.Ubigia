// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer;

public static class Factory
{
    public static TInstance Create<TInstance>(IExtensible options)
    {
        var container = new Container();

        foreach (var extension in options.Extensions)
        {
            extension.Initialize(container);
        }

        return container.GetInstance<TInstance>();
    }

    public static (TFirstInstance, TSecondInstance) Create<TFirstInstance, TSecondInstance>(IExtensible options)
    {
        var container = new Container();

        foreach (var extension in options.Extensions)
        {
            extension.Initialize(container);
        }

        return (container.GetInstance<TFirstInstance>(), container.GetInstance<TSecondInstance>());
    }
}
