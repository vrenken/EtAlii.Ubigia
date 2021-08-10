// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.MicroContainer;

    public class ContainersScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IContainerCreator, ContainerCreator>();
            container.Register<INextContainerIdentifierAlgorithm, NextContainerIdentifierFromFolderAlgorithm>();
            container.RegisterDecorator(typeof(INextContainerIdentifierAlgorithm), typeof(NextContainerIdentifierFromTimeAlgorithm));
            container.Register<ILatestEntryGetter, LatestEntryGetter>();
        }
    }
}
