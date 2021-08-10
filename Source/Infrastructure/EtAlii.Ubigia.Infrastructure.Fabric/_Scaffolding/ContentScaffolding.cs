// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using HashLib;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContentScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IContentSet, ContentSet>();

            container.Register<IContentGetter, ContentGetter>();
            container.Register<IContentPartGetter, ContentPartGetter>();
            container.Register<IContentStorer, ContentStorer>();
            container.Register<IContentPartStorer, ContentPartStorer>();
            container.Register(HashFactory.Checksum.CreateCRC64_ECMA);
        }
    }
}
