// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using EtAlii.xTechnology.MicroContainer;

public class ImmutablesScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IImmutableFolderManager>(services => services.GetInstance<IFolderManager>());
        container.Register<IImmutableFileManager>(services => services.GetInstance<IFileManager>());
    }
}
