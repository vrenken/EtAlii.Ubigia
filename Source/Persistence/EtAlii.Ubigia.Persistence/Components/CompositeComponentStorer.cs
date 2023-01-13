// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System;

internal class CompositeComponentStorer : ICompositeComponentStorer
{
    private readonly IPathBuilder _pathBuilder;
    private readonly INextCompositeComponentIdAlgorithm _nextCompositeComponentIdAlgorithm;
    private readonly IImmutableFolderManager _folderManager;

    public CompositeComponentStorer(
        IImmutableFolderManager folderManager,
        IPathBuilder pathBuilder,
        INextCompositeComponentIdAlgorithm nextCompositeComponentIdAlgorithm)
    {
        _folderManager = folderManager;
        _pathBuilder = pathBuilder;
        _nextCompositeComponentIdAlgorithm = nextCompositeComponentIdAlgorithm;
    }


    public void Store(ContainerIdentifier container, CompositeComponent component)
    {
        var componentName = ComponentHelper.GetName(component);

        container = ContainerIdentifier.Combine(container, componentName);

        if (component.Id != 0)
        {
            throw new InvalidOperationException("Unable to store composite component: Id already assigned");
        }

        var compositeComponentId = _nextCompositeComponentIdAlgorithm.Create(container);

        var folder = _pathBuilder.GetFolder(container);

        //var format = "Storing Composite component (Id:[2]) component to: [1]"
        //_logger.Verbose[format, componentName, folder, compositeComponentId]

        _folderManager.Create(folder);

        ComponentHelper.SetStored(component, false);
        _folderManager.SaveToFolder(component, compositeComponentId.ToString(), folder);
        ComponentHelper.SetStored(component, true);

        ComponentHelper.SetId(component, compositeComponentId);
    }
}
