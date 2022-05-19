// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Standard
{
    using EtAlii.xTechnology.MicroContainer;

    public class StandardStorageExtension : IExtension
    {
        private readonly string _baseFolder;

        public StandardStorageExtension(string baseFolder)
        {
            _baseFolder = baseFolder;
        }

        public void Initialize(IRegisterOnlyContainer container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new StandardFactoryScaffolding()
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
            container.RegisterInitializer<IPathBuilder>(pb => ((StandardPathBuilder)pb).Initialize(_baseFolder));
        }
    }
}
