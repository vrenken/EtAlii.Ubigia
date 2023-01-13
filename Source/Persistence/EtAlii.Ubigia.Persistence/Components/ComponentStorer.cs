// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

internal class ComponentStorer : IComponentStorer
{
    private readonly IPathBuilder _pathBuilder;
    private readonly IImmutableFolderManager _folderManager;

    public ComponentStorer(IImmutableFolderManager folderManager,
        IPathBuilder pathBuilder)
    {
        _folderManager = folderManager;
        _pathBuilder = pathBuilder;
    }

    public void Store<T>(ContainerIdentifier container, T component)
        where T : class, IComponent
    {
        var componentName = ComponentHelper.GetName(component);

        var folder = _pathBuilder.GetFolder(container);

        _folderManager.Create(folder);

        ComponentHelper.SetStored(component, false);
        _folderManager.SaveToFolder(component, componentName, folder);
        ComponentHelper.SetStored(component, true);
    }
}
